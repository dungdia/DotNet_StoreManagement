using DotNet_StoreManagement.Domain.entities.@base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DotNet_StoreManagement.Features.ProductAPI.dtos;

public class ProductFilterDTO : BaseFilter
{
    public string? ProductName { get; set; } 

    public string? Barcode { get; set; } 

    public decimal? Price { get; set; }

    public string? Unit { get; set; } 
    
    public DateOnly? CreatedAt { get; set; }

    public string? ProductImg { get; set; } 
}

public class BaseFilter
{
    public string? OrderBy { get; set; } 
    public string? SortBy { get; set; }

    // public BaseFilter()
    // {
    // }
    //
    // public BaseFilter(string OrderBy, string SortBy)
    // {
    //     this.OrderBy = OrderBy;
    //     this.SortBy = SortBy;
    // }
}
