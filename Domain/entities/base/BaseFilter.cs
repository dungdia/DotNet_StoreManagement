using System.Text.Json.Serialization;
using DotNet_StoreManagement.Domain.enums;

namespace DotNet_StoreManagement.Domain.entities.@base;

public class BaseFiler
{
    public OrderBy? OrderBy { get; set; }
    public string? SortBy { get; set; }
}