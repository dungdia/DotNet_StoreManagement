using System.Linq.Expressions;
using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using DotNet_StoreManagement.Features.ProductAPI.dtos;
using DotNet_StoreManagement.Features.ProductAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.persistence;
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

        query = query.Filter("ProductName", filterDto?.ProductName, FilterType.CONTAINS)
            .Filter("Barcode", filterDto?.Barcode, FilterType.CONTAINS)
            .Filter("Unit", filterDto?.Unit, FilterType.CONTAINS)
            .RangeValue("Price", filterDto?.MinPrice, filterDto?.MaxPrice);
            
        return await _repo.FindAllPageAsync(
            query,
            pageRequest.PageNumber,
            pageRequest.PageSize
        );
    }

    public async Task<Object?> getProductById(int id)
    {
        var product = await _repo.FindProductById(id);
        
        if (product == null) throw APIException.BadRequest("San pham khong ton tai");
        
        return product;
    }

    public async Task<Product> UploadProduct(ProductDTO dto)
    {
        var product = _mapper.Map<Product>(dto);
        product.CreatedAt = DateTime.Now;
        
        var existedBarcode = await _repo.FindBarcode(0, dto);
        if (existedBarcode != null) throw APIException.BadRequest("Barcode da ton tai");
        
        await _repo.AddAsync(product);
        var affectedRows = await _repo.SaveChangesAsync();
        
        if (affectedRows < 0) throw APIException.InternalServerError("Add failed");
        
        return product;
    }
    
    public async Task<Product?> EditProduct(int id, ProductDTO dto)
    {
        var product = await _repo.GetByIdAsync(id);
        
        if (product == null) throw APIException.BadRequest("San pham khong ton tai");

        var existedBarcode = await _repo.FindBarcode(id, dto);
        
        if (existedBarcode != null) throw APIException.BadRequest("Barcode da ton tai");
        
        await _repo.UpdateAsync(_mapper.Map(dto, product));
        var affectedRows = await _repo.SaveChangesAsync();
        
        if(affectedRows < 0) throw new ApplicationException("Cap nhat san pham loi");
        
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


