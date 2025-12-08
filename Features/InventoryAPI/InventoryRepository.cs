using System.Linq.Expressions;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.InventoryAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;
using DotNet_StoreManagement.SharedKernel.persistence.impl;

namespace DotNet_StoreManagement.Features.InventoryAPI;

[Repository]
public class InventoryRepository : DPARepository<Inventory, int>, IInventoryRepository
{
    private readonly AppDbContext _context;

    public InventoryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}