using System.Collections.ObjectModel;
using System.Reflection;
using DotNet_StoreManagement.Domain.entities;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Serilog;
using Serilog.Core;

namespace DotNet_StoreManagement.SharedKernel.persistence;

public class AppDbContext : BaseContext
{
    private readonly string _connectionString;
    
    public AppDbContext(
        DbContextOptions<BaseContext> options,
        IConfiguration configuration
        ) : base(options)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
    }
    
    public ICollection<Dictionary<String, Object>> executeSqlRaw(String query, params Object[] parameters)
    {
        var dictResult = new Collection<Dictionary<String, Object>>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        using var command = new MySqlCommand(query, connection);
        for (int i = 0; i < parameters.Length; ++i)
        {
            command.Parameters.AddWithValue($"{i}", parameters[i]);
        }

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var dict = Enumerable.Range(0, reader.FieldCount)
                .ToDictionary(
                    i => reader.GetName(i),
                    i => reader.IsDBNull(i) ? null : reader.GetValue(i)
                );
            dictResult.Add(dict);
        }

        return dictResult;
    }
    
    public async Task<ICollection<Dictionary<String, Object>>> executeSqlRawAsync(String query, params Object[] parameters)
    {
        var dictResult = new Collection<Dictionary<String, Object>>();
    
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();
    
        await using var command = new MySqlCommand(query, connection);
        for (int i = 0; i < parameters.Length; ++i)
        {
            command.Parameters.AddWithValue($"{i}", parameters[i]);
        }
    
        await using var reader = await command.ExecuteReaderAsync();
    
        while (await reader.ReadAsync())
        {
            var dict = Enumerable.Range(0, reader.FieldCount)
                .ToDictionary(
                    i => reader.GetName(i),
                    i => reader.IsDBNull(i) ? null : reader.GetValue(i)
                );
            dictResult.Add(dict);
        }
    
        return dictResult;
    }
    
    public async Task<ICollection<T>> executeSqlRawAsync<T>(
        string query,
        params object[] parameters
    ) where T : new()
    {
        var result = new List<T>();

        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(query, connection);

        for (int i = 0; i < parameters.Length; i++)
        {
            command.Parameters.AddWithValue($"@p{i}", parameters[i] ?? DBNull.Value);
        }

        await using var reader = await command.ExecuteReaderAsync();

        var isSimpleType = IsSimpleType(typeof(T));

        while (await reader.ReadAsync())
        {
            if (isSimpleType)
            {
                var val = reader.GetValue(0);
                if (val == DBNull.Value)
                {
                    result.Add(default!);
                }
                else
                {
                    result.Add((T)Convert.ChangeType(val, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T)));
                }

                continue;
            }
            
            var obj = new T();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i);

                var prop = props.FirstOrDefault(p =>
                    string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));

                if (prop == null || !prop.CanWrite)
                    continue;

                var value = reader.GetValue(i);
                if (value == DBNull.Value)
                    continue;

                var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                prop.SetValue(obj, Convert.ChangeType(value, targetType));
            }

            result.Add(obj);
        }

        return result;
    }
    
    private static bool IsSimpleType(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;

        return type.IsPrimitive
               || type.IsEnum
               || type == typeof(string)
               || type == typeof(decimal)
               || type == typeof(DateTime)
               || type == typeof(Guid);
    }
}