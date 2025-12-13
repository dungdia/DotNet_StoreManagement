using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.ProductAPI.dtos;
using DotNet_StoreManagement.SharedKernel.persistence.impl;

namespace DotNet_StoreManagement.Features.ProductAPI.impl;

public interface IProductRepository : IDPARepository<Product, int>
{
    IQueryable<Product> GetQueryable();
    Task<Product?> FindBarcode(int id, ProductDTO product);
    Task<T?> FindProductDetail<T>(int id) where T : class, new();
    Task<Page<T>> SearchProductPageable<T>(ProductSearchDTO dto, PageRequest pageRequest) where T : class, new();
}