using System.ComponentModel.DataAnnotations;

namespace DotNet_StoreManagement.Features.OrderAPI.dtos;

public class OnlineOrderDTO
{
    [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public string? PromoCode { get; set; }

    [Required]
    public string PaymentMethod { get; set; } = "cash";

    public string? Note { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "Đơn hàng phải có ít nhất 1 sản phẩm")]
    public List<CreateOrderItemDTO> Items { get; set; } = new();
}

public class CreateOrderItemDTO
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(1, 1000, ErrorMessage = "Số lượng phải lớn hơn 0")]
    public int Quantity { get; set; }
}