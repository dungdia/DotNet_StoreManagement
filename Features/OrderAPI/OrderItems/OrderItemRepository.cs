using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.OrderAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;

namespace DotNet_StoreManagement.Features.OrderAPI.OrderItems;

[Repository]
public class OrderItemRepository : DPARepository<OrderItem, int>, IOrderItemRepository
{
    private readonly AppDbContext _context;
    public OrderItemRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
