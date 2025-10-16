using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.enums;
using DotNet_StoreManagement.Features.ProductAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;
using Microsoft.EntityFrameworkCore;

namespace DotNet_StoreManagement.Features.ProductAPI;

[Repository]
public class ProductRepository : DPARepository<Product, int>, IProductRepository
{
    public AppDbContext _context { get; set; }

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }


    public IQueryable<Product> GetQueryable()
    {
        return _context.Products; 
    }
}