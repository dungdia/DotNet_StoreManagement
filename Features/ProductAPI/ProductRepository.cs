using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.ProductAPI.dtos;
using DotNet_StoreManagement.Features.ProductAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
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
    
    public async Task<T?> FindProductDetail<T>(int id) where T : class, new()
    {
        string query = """
                       SELECT 
                        p.product_name as productName, p.barcode, p.price, p.unit, 
                        s.name as supplierName,
                        c.category_name as categoryName
                       FROM products p 
                       JOIN suppliers s ON s.supplier_id = p.supplier_id
                       JOIN categories c ON c.category_id = p.category_id
                       WHERE p.product_id = ?;
                       """;
        try
        {
            var result = await _context.executeSqlRawAsync<T>(query, id);
            return result.FirstOrDefault();
        }
        catch (Exception e)
        {
            throw APIException.InternalServerError(e.Message);
        }
    }
}