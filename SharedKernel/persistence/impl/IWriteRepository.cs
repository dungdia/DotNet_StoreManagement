namespace DotNet_StoreManagement.SharedKernel.persistence.impl;

public interface IWriteRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    public Task<int> AddAndSaveAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    public Task<int> UpdateAndSaveAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    public Task<int> DeleteAndSaveAsync(TEntity entity);
    Task<int> SaveChangesAsync();
}