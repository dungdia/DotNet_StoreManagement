using System.Linq.Expressions;
using DotNet_StoreManagement.Domain.entities.@base;

namespace DotNet_StoreManagement.SharedKernel.persistence.impl;

public interface IDPARepository<TEntity, TKey> : 
    IReadRepository<TEntity, TKey>,
    IWriteRepository<TEntity> where TEntity : class
{
    public Task<Page<TEntity>> FindAllPageAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int pageNumber = 1,
        int pageSize = 5
    );
}