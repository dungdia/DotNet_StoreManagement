using DotNet_StoreManagement.Domain.enums;

namespace DotNet_StoreManagement.Features.OrderAPI.dtos;

public class OrderFilterDTO
{
    public string? SearchTerm { get; set; } = "";

    public List<int>? CustomerIds { get; set; } = new();
    public List<int>? UserIds { get; set; } = new();
    public DateTime? OrderDate { get; set; }
    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }
    public OrderStatus? Status { get; set; }
    public string? SortBy { get; set; } = "OrderDate";
    public bool SortDescending { get; set; } = false;

}
