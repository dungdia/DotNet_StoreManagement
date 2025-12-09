using System.Linq.Expressions;

namespace DotNet_StoreManagement.SharedKernel.persistence.impl;

public interface IReadRepository<TEntity, TKey> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<ICollection<TEntity>> GetAllAsync();
    Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
}