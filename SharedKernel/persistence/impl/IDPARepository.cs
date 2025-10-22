using System.Linq.Expressions;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;

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
    
    public Task<Page<TEntity>> FindAllPageAsync_V2(
        IQueryable<TEntity> filter,
        String? sortBy,
        OrderBy? orderBy,
        int pageNumber = 1,
        int pageSize = 5
    );

    public IQueryable<TEntity> FilterString(
        IQueryable<TEntity> query,
        string key,
        string? value,
        FilterType? type);
    
    // TODO: can implement cac filter khac
}