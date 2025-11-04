using System.Linq.Expressions;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;

namespace DotNet_StoreManagement.SharedKernel.persistence.impl;

public interface IDPARepository<TEntity, TKey> : 
    IReadRepository<TEntity, TKey>,
    IWriteRepository<TEntity> where TEntity : class
{
    public Task<Page<TEntity>> FindAllPageAsync(
        IQueryable<TEntity> filter,
        int pageNumber = 1,
        int pageSize = 5
    );
}