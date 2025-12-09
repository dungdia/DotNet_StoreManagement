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
    
    public async Task<ICollection<T>> executeSqlRawAsync<T>(String query, params Object[] parameters) where T : class, new()
    {
        var result = new List<T>();
    
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();
    
        await using var command = new MySqlCommand(query, connection);
        for (int i = 0; i < parameters.Length; ++i)
        {
            command.Parameters.AddWithValue($"{i}", parameters[i]);
        }
        
        await using var reader = await command.ExecuteReaderAsync();
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var columnNames = Enumerable.Range(0, reader.FieldCount)
            .Select(reader.GetName)
            .ToList();

    
        while (await reader.ReadAsync())
        {
            var obj = new T();
            foreach (var prop in props)
            {
                var columnName = columnNames
                    .FirstOrDefault(cn => string.Equals(cn, prop.Name, StringComparison.OrdinalIgnoreCase));
                if(columnName == null) 
                    continue;

                var val = reader[columnName];
                if(val == DBNull.Value) continue;
                
                prop.SetValue(obj, Convert.ChangeType(val, prop.PropertyType));
            }
            result.Add(obj);
        }
    
        return result;
    }
}