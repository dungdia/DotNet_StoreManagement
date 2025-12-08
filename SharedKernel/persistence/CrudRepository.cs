using System.Linq.Expressions;
using DotNet_StoreManagement.SharedKernel.persistence.impl;
using Microsoft.EntityFrameworkCore;

namespace DotNet_StoreManagement.SharedKernel.persistence;

public class CrudRepository<TEntity, TKey> : ICrudRepository<TEntity, TKey> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public CrudRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }
    
    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<int> AddAndSaveAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<int> UpdateAndSaveAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(TEntity entity) 
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<int> DeleteAndSaveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}