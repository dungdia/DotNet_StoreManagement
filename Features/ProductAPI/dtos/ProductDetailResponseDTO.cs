namespace DotNet_StoreManagement.Features.ProductAPI.dtos;

public class ProductDetailResponseDTO
{
    public int ProductId { get; set; }
    
    public int? SupplierId { get; set; }
    
    public int? CategoryId { get; set; }
    
    public string ProductName { get; set; }
    
    public string Barcode { get; set; }
    
    public string Price { get; set; }
    
    public string Unit { get; set; }
    
    public string? ProductImg { get; set; }
    
    public string SupplierName { get; set; }
    
    public string CategoryName { get; set; }
}