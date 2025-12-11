using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DotNet_StoreManagement.Features.ProductAPI.dtos;

public class ProductFilterDTO
{
    public string? SearchTerm { get; set; } = "";
    
    public List<int>? CategoryIds { get; set; } = new();
    
    public decimal? MinPrice { get; set; }
    
    public decimal? MaxPrice { get; set; }

    public string? SortBy { get; set; } = "CreatedAt";
    public bool SortDescending { get; set; } = false;
}


