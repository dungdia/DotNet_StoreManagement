using DotNet_StoreManagement.Domain.entities;
namespace DotNet_StoreManagement.Features.ProductAPI.dtos;

public class ProductResponseDTO
{
    public string ProductName { get; set; } = null!;
    
    public string Barcode { get; set; }
    
    public decimal Price { get; set; }
    
    public string Unit { get; set; }
    
    public string? ProductImg { get; set; }
    
    public Supplier Supplier { get; set; }
}