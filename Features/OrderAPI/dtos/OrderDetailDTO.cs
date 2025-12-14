using DotNet_StoreManagement.Domain.entities;

namespace DotNet_StoreManagement.Features.OrderAPI.dtos
{
    public class OrderDetailDTO
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int PromoId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }

        public OrderDetailDTO(OrderDTO order, List<OrderItemDTO> orderItems)
        {
            OrderId = order.OrderId;
            CustomerId = order.CustomerId;
            UserId = order.UserId;
            PromoId = order.PromoId;
            TotalAmount = order.TotalAmount;
            DiscountAmount = order.DiscountAmount;
            OrderItems = orderItems;
        }
    }
}
