using System.Linq.Expressions;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.OrderAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;
using Microsoft.EntityFrameworkCore;

namespace DotNet_StoreManagement.Features.OrderAPI.OrderItems;

[Repository]
public class OrderItemRepository : DPARepository<OrderItem, int>, IOrderItemRepository
{
    private readonly AppDbContext _context;
    public OrderItemRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<OrderItem>> FindWithProductAsync(Expression<Func<OrderItem, bool>> predicate)
    {
        return await _context.OrderItems
            .Include(oi => oi.Product)
            .Where(predicate)
            .ToListAsync();
    }
}
