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
}