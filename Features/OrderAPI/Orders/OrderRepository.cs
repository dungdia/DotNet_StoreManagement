using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.OrderAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;

namespace DotNet_StoreManagement.Features.OrderAPI.Orders;

[Repository]
public class OrderRepository : DPARepository<Order, int>, IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
