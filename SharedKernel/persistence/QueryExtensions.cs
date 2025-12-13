using DotNet_StoreManagement.Domain.enums;

namespace DotNet_StoreManagement.SharedKernel.persistence;

public static class QueryExtensions
{
    public static (string Query, List<object> Parameters) SearchFilter(
        this (string Query, List<object> Parameters) queryData,
        bool condition,
        string filterClause,
        params object[] parameters)
    {
        if (condition && parameters.Length > 0)
        {
            queryData.Query += $" AND {filterClause}";
            queryData.Parameters.AddRange(parameters);
        }
        return queryData;
    }
    
    public static (string Query, List<object> Parameters) RangeFilter<T>(
        this (string Query, List<object> Parameters) queryData,
        string column,
        T? minValue,
        T? maxValue) where T : struct
    {
        if (minValue != null && maxValue != null)
        {
            queryData.Query += $" AND {column} BETWEEN ? AND ?";
            queryData.Parameters.Add(minValue.Value);
            queryData.Parameters.Add(maxValue.Value);
        }

        return queryData;
    }
    
    public static (string Query, List<object> Parameters) ToCountQuery(
        this (string Query, List<object> Parameters) queryData)
    {
        var fromIndex = queryData.Query.IndexOf("FROM", StringComparison.OrdinalIgnoreCase);
        if (fromIndex < 0)
            return ("SELECT COUNT(*) AS COUNT ", queryData.Parameters);

        var countQuery = "SELECT COUNT(*) AS COUNT " + queryData.Query[fromIndex..];

        return (countQuery, queryData.Parameters);
    }
    
    public static (string Query, List<object> Parameters) InFilter(
        this (string Query, List<object> Parameters) queryData,
        string column,
        string? values)
    {
        if (string.IsNullOrEmpty(values)) 
            return queryData;
        
        var valueList = values.Split(',')
            .Select(v => v.Trim())
            .Where(v => !string.IsNullOrEmpty(v))
            .ToList();

        if (valueList.Count == 0)
            return queryData;

        var parameters = string.Join(", ", valueList.Select(_ => "?"));
        queryData.Query += $" AND {column} IN ({parameters})";
        queryData.Parameters.AddRange(valueList);

        return queryData;
    }

    public static (string Query, List<object> Parameters) AddSorting(
        this (string Query, List<object> Parameters) queryData,
        string sortBy,
        bool isDescending = false)
    {
        if (!string.IsNullOrEmpty(sortBy))
        {
            var order = isDescending ? OrderBy.ASC.ToString() : OrderBy.DESC.ToString();
            queryData.Query += $" ORDER BY {sortBy} {order}";
        }
        return queryData;
    }

    public static (string Query, List<object> Parameters) AddPagination(
        this (string Query, List<object> Parameters) queryData,
        int pageNumber,
        int pageSize)
    {
        queryData.Query += " LIMIT ? OFFSET ?";
        queryData.Parameters.Add(pageSize);
        queryData.Parameters.Add((pageNumber - 1) * pageSize);
        return queryData;
    }
}