using DotNet_StoreManagement.Domain.enums;

namespace DotNet_StoreManagement.Features.OrderAPI.dtos;

public class OrderStatusRequest
{
    public OrderStatus? Status { get; set; }
}
