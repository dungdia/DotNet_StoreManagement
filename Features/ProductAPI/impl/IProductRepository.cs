using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.ProductAPI.dtos;
using DotNet_StoreManagement.SharedKernel.persistence.impl;

namespace DotNet_StoreManagement.Features.ProductAPI.impl;

public interface IProductRepository : IDPARepository<Product, int>
{
    IQueryable<Product> GetQueryable();
    Task<Product?> FindBarcode(int id, ProductDTO product);
}