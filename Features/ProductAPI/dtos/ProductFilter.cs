using DotNet_StoreManagement.Domain.entities.@base;

namespace DotNet_StoreManagement.Features.ProductAPI.dtos;

public class ProductFilter
{
    public string? ProductName { get; set; } = null!;

    public string? Barcode { get; set; }

    public decimal Price { get; set; }

    public string? Unit { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ProductImg { get; set; }
}