namespace DotNet_StoreManagement.Features.ProductAPI.dtos;

public class ProductDetailResponseDTO
{
    public string ProductName { get; set; }
    public string Barcode { get; set; }
    public string Price { get; set; }
    public string Unit { get; set; }
    public string? ProductImg { get; set; }
    public string SupplierName { get; set; }
    public string CategoryName { get; set; }
}