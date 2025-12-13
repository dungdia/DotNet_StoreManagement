using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
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
    
    public async Task<Page<T>> SearchProductPageable<T>(ProductSearchDTO dto, PageRequest pageRequest) where T : class, new()
    {
        try
        {
            string query = """
                              SELECT 
                               p.product_id as productId, p.product_name as productName, p.barcode, p.price, p.unit, 
                               s.name as supplierName,
                               c.category_name as categoryName
                              FROM products p 
                              LEFT JOIN suppliers s ON s.supplier_id = p.supplier_id
                              LEFT JOIN categories c ON c.category_id = p.category_id
                              WHERE 1=1
                           """;
        
            var parameters = new List<object>();
            var queryData = (query, parameters);
        
            Console.WriteLine(dto.Category);
            queryData = queryData
                .SearchFilter(!string.IsNullOrEmpty(dto.ProductName), "p.product_name LIKE ?", $"{dto.ProductName}%")
                .SearchFilter(!string.IsNullOrEmpty(dto.Barcode), "p.barcode LIKE ?", $"{dto.Barcode}%")
                .SearchFilter(!string.IsNullOrEmpty(dto.Unit), "p.unit LIKE ?", $"{dto.Unit}%")
                .RangeFilter("p.price", dto.MinPrice, dto.MaxPrice)
                .RangeFilter("p.created_at", dto.StartDate, dto.EndDate)
                .InFilter("c.category_name", dto.Category);
            
            var countQuery = queryData.ToCountQuery();
            var totalElements = await _context.executeSqlRawAsync<int>(countQuery.Query, countQuery.Parameters.ToArray());
            
            // Thuc hien phan trang sau khi dem tong so phan tu 
            queryData = queryData.AddPagination(pageRequest.PageNumber, pageRequest.PageSize);
            
            var result = await _context.executeSqlRawAsync<T>(queryData.query, queryData.parameters.ToArray());
        
            return new Page<T>
            {
                Content = result,
                PageNumber = pageRequest.PageNumber,
                PageSize = pageRequest.PageSize,
                TotalElements = totalElements.FirstOrDefault(),
                TotalPages = (int)Math.Ceiling((double)totalElements.FirstOrDefault() / pageRequest.PageSize)
            };
        }
        catch (Exception e)
        {
            throw APIException.InternalServerError(e.Message);
        }
    }
}