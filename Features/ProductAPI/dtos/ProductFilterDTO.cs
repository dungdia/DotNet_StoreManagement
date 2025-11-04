using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DotNet_StoreManagement.Features.ProductAPI.dtos;

public class ProductFilterDTO
{
    public string? ProductName { get; set; } 

    public string? Barcode { get; set; } 

    public decimal? MinPrice { get; set; }
    
    public decimal? MaxPrice { get; set; }

    public string? Unit { get; set; } 
    
    public DateOnly? CreatedAt { get; set; }

    public string? ProductImg { get; set; } 
}


