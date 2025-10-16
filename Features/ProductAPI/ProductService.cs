using System.Linq.Expressions;
using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using DotNet_StoreManagement.Features.ProductAPI.dtos;
using DotNet_StoreManagement.Features.ProductAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DotNet_StoreManagement.Features.ProductAPI;

[Service]
public class ProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<Page<Product>> getPageableProduct(ProductFilterDTO? filterDto, PageRequest pageRequest)
    {
        IQueryable<Product> query = _repo.GetQueryable();
        
        query = _repo.FilterString(query, "ProductName", filterDto?.ProductName, FilterType.CONTAINS);
        query = _repo.FilterString(query, "Barcode", filterDto?.Barcode, FilterType.CONTAINS);
        query = _repo.FilterString(query, "Unit", filterDto?.Unit, FilterType.CONTAINS);
        
        return await _repo.FindAllPageAsync_V2(
            query,
            filterDto?.SortBy,
            filterDto?.OrderBy,
            pageRequest.PageNumber,
            pageRequest.PageSize
        );
    }

    public async Task<Product> UploadProduct(ProductDTO dto)
    {
        var product = _mapper.Map<Product>(dto);
        product.CreatedAt = DateTime.Now;
        
        await _repo.AddAsync(product);
        var affectedRows = await _repo.SaveChangesAsync();
        
        if (affectedRows < 0) throw APIException.InternalServerError("Add failed");
        
        return product;
    }
    
    public async Task<Product?> EditProduct(int id, ProductDTO dto)
    {
        var product = await _repo.GetByIdAsync(id);
        
        if (product == null) throw APIException.BadRequest("Can't find product");
        await _repo.UpdateAsync(_mapper.Map(dto, product));
        var affectedRows = await _repo.SaveChangesAsync();
        
        if(affectedRows < 0) throw new ApplicationException("update failed");
        
        return product;
    }
    
    public async Task<Product> DeleteProduct(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        
        if (product == null) throw APIException.BadRequest("Can't find product");
        await _repo.DeleteAsync(product);
        var affectedRows = await _repo.SaveChangesAsync();
        
        if(affectedRows < 0) throw APIException.InternalServerError("Add failed");
        return product;
    }
}


