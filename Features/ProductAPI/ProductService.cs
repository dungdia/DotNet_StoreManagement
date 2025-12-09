using System.Linq.Expressions;
using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using DotNet_StoreManagement.Features.ProductAPI.dtos;
using DotNet_StoreManagement.Features.ProductAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.persistence;

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

    // public async Task<dynamic?> getProductById(int id)
    // {
    //     var product = await _repo.FindProductById(id);
    //
    //     if (product == null) throw APIException.BadRequest($"Mã sản phẩm không tồn tại");
    //
    //     return product;
    // }

    public async Task<ProductDetailResponseDTO> getProductDetail(int id)
    {
        var product = await _repo.FindProductDetail<ProductDetailResponseDTO>(id);
    
        if (product == null) throw APIException.BadRequest($"Mã sản phẩm không tồn tại");
    
        return product;
    }

    public async Task<Product> UploadProduct(ProductDTO dto)
    {
        var product = _mapper.Map<Product>(dto);
        product.CreatedAt = DateTime.Now;
        
        var existedBarcode = await _repo.FindBarcode(0, dto);
        if (existedBarcode != null) throw APIException.BadRequest("Barcode đã tồn tại");
        
        await _repo.AddAsync(product);
        var affectedRows = await _repo.SaveChangesAsync();
        
        if (affectedRows < 0) throw APIException.InternalServerError("Thêm sản phẩm thất bại");
        
        return product;
    }
    
    public async Task<Product?> editProduct(int id, ProductDTO dto)
    {
        var product = await _repo.GetByIdAsync(id);
        
        if (product == null) throw APIException.BadRequest("Sản pẩm không tồn tại");

        var existedBarcode = await _repo.FindBarcode(id, dto);
        if (existedBarcode != null) throw APIException.BadRequest("Barcode đã tồn tại");
        
        await _repo.UpdateAsync(_mapper.Map(dto, product));
        var affectedRows = await _repo.SaveChangesAsync();
        
        if(affectedRows < 0) throw new ApplicationException("Cập nhật sản phẩm thất bại");
        
        return product;
    }
    
    public async Task<Product> deleteProduct(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        
        if (product == null) throw APIException.BadRequest("Sản phẩm không tồn tại");
        
        await _repo.DeleteAsync(product);
        var affectedRows = await _repo.SaveChangesAsync();
        
        if(affectedRows < 0) throw APIException.InternalServerError("Xóa sản phẩm thất bại");
        return product;
    }
}


