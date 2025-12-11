using System.Linq.Expressions;
using DotNet_StoreManagement.Domain.enums;
using Microsoft.EntityFrameworkCore;

namespace DotNet_StoreManagement.SharedKernel.persistence;

public static class FilterExtensions
{
    public static IQueryable<TEntity> Filter<TEntity>(
        this IQueryable<TEntity> query,
        string key,
        string? value,
        FilterType? type)
    {
        if (string.IsNullOrEmpty(value))
            return query;

        value = value.ToLower();

        return type switch
        {
            FilterType.EQUAL => query.Where(p => EF.Property<string>(p, key).ToLower() == value),
            FilterType.CONTAINS => query.Where(p => EF.Property<string>(p, key).ToLower().Contains(value)),
            FilterType.START_WITH => query.Where(p => EF.Property<string>(p, key).ToLower().StartsWith(value)),
            _ => query
        };
    }
    
    public static IQueryable<TEntity> RangeValue<TEntity, TValue>(
        this IQueryable<TEntity> query,
        string key,
        TValue? minValue,
        TValue? maxValue
    ) where TValue : struct, IComparable
    {
        var parameter = Expression.Parameter(typeof(TEntity), "p");
        var property = Expression.Property(parameter, key);

        if (minValue.HasValue)
        {
            var min = Expression.Constant(minValue.Value);
            var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, min);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(greaterThanOrEqual, parameter);
            query = query.Where(lambda);
        }

        if (maxValue.HasValue)
        {
            var max = Expression.Constant(maxValue.Value);
            var lessThanOrEqual = Expression.LessThanOrEqual(property, max);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(lessThanOrEqual, parameter);
            query = query.Where(lambda);
        }

        return query;
    }
    
    public static IQueryable<TEntity> RangeDate<TEntity>(
        this IQueryable<TEntity> query,
        string key,
        DateTime? startDate,
        DateTime? endDate
    )
    {
        if (startDate.HasValue)
            query = query.Where(p => EF.Property<DateTime>(p, key) >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(p => EF.Property<DateTime>(p, key) <= endDate.Value);

        return query;
    }
    
    public static IQueryable<TEntity> OrderBy<TEntity>(
        this IQueryable<TEntity> query,
        string? sortBy,
        string? orderBy
    )
    {
        if (string.IsNullOrEmpty(sortBy))
            return query;

        return orderBy == "DESC"
            ? query.OrderByDescending(p => EF.Property<object>(p, sortBy))
            : query.OrderBy(p => EF.Property<object>(p, sortBy));
    }

    public static IQueryable<TEntity> SearchMultipleField<TEntity>(
        this IQueryable<TEntity> query,
        string? searchTerm,
        params string[] keys
    )
    {
        if( string.IsNullOrEmpty(searchTerm) || keys.Length == 0)
            return query;

        searchTerm = searchTerm.Trim().ToLower();
        var parameter = Expression.Parameter(typeof(TEntity), "p");
        Expression? finalExpression = null;

        foreach (var propName in keys)
        {
            // p.PropName
            var property = Expression.Property(parameter, propName);

            // .ToLower()
            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            var toLowerCall = Expression.Call(property, toLowerMethod!);

            // .Contains(searchTerm)
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsCall = Expression.Call(toLowerCall, containsMethod!, Expression.Constant(searchTerm));

            // Nối các điều kiện bằng OR: (Name contains X) OR (Barcode contains X)
            if (finalExpression == null)
            {
                finalExpression = containsCall;
            }
            else
            {
                finalExpression = Expression.OrElse(finalExpression, containsCall);
            }
        }

        var lambda = Expression.Lambda<Func<TEntity, bool>>(finalExpression!, parameter);
        return query.Where(lambda);
    }

    public static IQueryable<TEntity> FilterByList<TEntity, TValue>(
        this IQueryable<TEntity> query,
        string propertyName,
        IEnumerable<TValue>? values)
    {
        if (values == null || !values.Any())
            return query;

        var parameter = Expression.Parameter(typeof(TEntity), "p");
        var property = Expression.Property(parameter, propertyName);

        var valuesList = values.ToList();
        var constant = Expression.Constant(valuesList, typeof(List<TValue>));
        var containsMethod = typeof(List<TValue>).GetMethod("Contains", new[] { typeof(TValue) });

        if (containsMethod == null)
            throw new InvalidOperationException($"Method Contains not found for type {typeof(TValue)}");

        Expression body;

        // KIỂM TRA: Nếu Property là Nullable (int?) còn TValue là Non-Nullable (int)
        // Ví dụ: Property là int?, nhưng List là List<int>
        var propertyType = property.Type;
        var isNullableProperty = Nullable.GetUnderlyingType(propertyType) != null;

        // Nếu Property là int? mà TValue là int -> Phải xử lý đặc biệt
        if (isNullableProperty && Nullable.GetUnderlyingType(propertyType) == typeof(TValue))
        {
            // Logic: (p.CategoryId.HasValue && list.Contains(p.CategoryId.Value))

            // 1. Check HasValue
            var hasValue = Expression.Property(property, "HasValue");

            // 2. Lấy .Value
            var valueAccess = Expression.Property(property, "Value");

            // 3. Gọi Contains với .Value (lúc này đã là int chuẩn)
            var containsCall = Expression.Call(constant, containsMethod, valueAccess);

            // 4. Kết hợp lại bằng AND
            body = Expression.AndAlso(hasValue, containsCall);
        }
        else
        {
            // Trường hợp bình thường (Type khớp nhau)
            // Logic: list.Contains(p.CategoryId)
            body = Expression.Call(constant, containsMethod, property);
        }

        var lambda = Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        return query.Where(lambda);
    }

    public static IQueryable<TEntity> ApplyOrdering<TEntity>(
    this IQueryable<TEntity> query,
    string? sortBy,
    bool isDescending)
    {
        if (string.IsNullOrWhiteSpace(sortBy))
        {
            // Mặc định sort theo Key (thường là Id) nếu không truyền gì
            // Hoặc bạn có thể return query luôn tùy logic
            return query;
        }

        // Tự động viết hoa chữ cái đầu để khớp với Property trong C# (Price, ProductName)
        // Ví dụ: gửi "price" -> thành "Price"
        string propertyName = char.ToUpper(sortBy[0]) + sortBy.Substring(1);

        // Kiểm tra xem Property có tồn tại trong Entity không (tránh lỗi crash)
        var type = typeof(TEntity);
        var property = type.GetProperty(propertyName,
            System.Reflection.BindingFlags.IgnoreCase |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);

        if (property == null)
        {
            // Nếu client gửi tên cột bậy bạ -> bỏ qua hoặc sort mặc định
            return query;
        }

        // --- Đoạn dưới dùng lại logic Expression Tree cũ ---
        var parameter = System.Linq.Expressions.Expression.Parameter(type, "p");
        var propertyAccess = System.Linq.Expressions.Expression.MakeMemberAccess(parameter, property);
        var orderByExp = System.Linq.Expressions.Expression.Lambda(propertyAccess, parameter);

        string methodName = isDescending ? "OrderByDescending" : "OrderBy";

        var resultExp = System.Linq.Expressions.Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { type, property.PropertyType },
            query.Expression,
            System.Linq.Expressions.Expression.Quote(orderByExp)
        );

        return query.Provider.CreateQuery<TEntity>(resultExp);
    }

}