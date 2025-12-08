using AutoMapper;
using DotNet_StoreManagement.Domain.entities;

namespace DotNet_StoreManagement.Features.OrderAPI.dtos;

[AutoMap(typeof(Order), ReverseMap = true)]
public class OrderDTO
{
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public int PromoId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
}
