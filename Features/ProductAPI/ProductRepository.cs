using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.ProductAPI.dtos;
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

    public async Task<Product?> FindBarcode(int id, ProductDTO product)
    {
        if (id == 0)
        {
            return await (
                from p in _context.Products
                where p.Barcode == product.Barcode
                select p
            ).FirstOrDefaultAsync();
        }

        return await (
            from p in _context.Products
            where p.Barcode == product.Barcode && p.ProductId != id
            select p
        ).FirstOrDefaultAsync();
    }
    
    public async Task<Object?> FindProductById(int id)
    {
        var result = await (from p in _context.Products
                join s in _context.Suppliers on p.SupplierId equals s.SupplierId
                where p.ProductId == id
                select new
                {
                    p.ProductId,
                    p.ProductName,
                    p.Barcode,
                    p.Unit,
                    p.Price,
                    p.CreatedAt,
                    p.ProductImg,
                    Supplier = new
                    {
                        s.Name,
                        s.Phone,
                        s.Email,
                        s.Address
                    }
                })
            .FirstOrDefaultAsync();
        return result;
    }
}