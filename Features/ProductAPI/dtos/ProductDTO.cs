using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DotNet_StoreManagement.Domain.entities;

namespace DotNet_StoreManagement.Features.ProductAPI.dtos;

[AutoMap(typeof(Product), ReverseMap = true)]
public class ProductDTO
{
    [Required(ErrorMessage = "Nhập tên sản phẩm")]
    public string ProductName { get; set; } = null!;
    
    [Required(ErrorMessage = "Nhập mã sản phẩm")]
    public string Barcode { get; set; }
    
    [Range(1, Double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Nhập đơn vị")]
    public string Unit { get; set; }
    
    // [Required(ErrorMessage = "Nhập đường dẫn ảnh sản phẩm")]
    public string? ProductImg { get; set; }
    
    public int? SupplierId { get; set; }
}