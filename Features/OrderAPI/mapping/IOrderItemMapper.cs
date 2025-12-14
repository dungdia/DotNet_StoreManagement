using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.OrderAPI.dtos;

namespace DotNet_StoreManagement.Features.OrderAPI.mapping
{
    public interface IOrderItemMapper
    {
        OrderItemDTO ToDTO(OrderItem orderItem);
        OrderItem ToEntity(OrderItemDTO dto);
    }
}
