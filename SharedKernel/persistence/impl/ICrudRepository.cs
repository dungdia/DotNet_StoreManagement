namespace DotNet_StoreManagement.SharedKernel.persistence.impl;

public interface ICrudRepository<TEntity, TKey> : 
    IReadRepository<TEntity, TKey>,
    IWriteRepository<TEntity> where TEntity : class
{
}