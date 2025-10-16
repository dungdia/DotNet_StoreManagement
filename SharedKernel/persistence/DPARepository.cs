﻿using System.Linq.Expressions;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.SharedKernel.persistence.impl;
using Microsoft.EntityFrameworkCore;

namespace DotNet_StoreManagement.SharedKernel.persistence;

public class DPARepository<TEntity, TKey> : IDPARepository<TEntity, TKey> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public DPARepository(AppDbContext context)
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

    public async Task<Page<TEntity>> FindAllPageAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int pageNumber = 1,
        int pageSize = 5
    )
    {
        IQueryable<TEntity> query = _dbSet;
        
        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        var total = await query.CountAsync();

        if (orderBy != null)
        {
            query = orderBy(query);
        }
        
        var content = await query
            .Skip((pageNumber - 1) * pageNumber)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
        
        return new Page<TEntity>
        {
            Content = content,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = total
        };
    }

    public async Task<Page<TEntity>> FindAllPageAsync_V2(IQueryable<TEntity> filter, string sortBy, string orderBy, int pageNumber = 1, int pageSize = 5)
    {
        if (!String.IsNullOrEmpty(sortBy) && !String.IsNullOrEmpty(orderBy) && orderBy.Contains("desc"))
        {
            filter = filter.OrderByDescending(p => EF.Property<object>(p, sortBy));   
        }

        var totalPages = await filter.CountAsync();
        
        var content = await filter
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new Page<TEntity>
        {
            Content = content,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages
        };
    }
}