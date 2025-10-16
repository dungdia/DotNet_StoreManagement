using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.SharedKernel.persistence;
using DotNet_StoreManagement.SharedKernel.persistence.impl;
using Microsoft.EntityFrameworkCore;

namespace DotNet_StoreManagement.Features.ProductAPI.impl;

public interface IProductRepository : IDPARepository<Product, int>
{
    IQueryable<Product> GetQueryable();
}