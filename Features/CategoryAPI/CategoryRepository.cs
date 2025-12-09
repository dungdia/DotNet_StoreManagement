using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.CategoryAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;

namespace DotNet_StoreManagement.Features.CategoryAPI;

[Repository]
public class CategoryRepository : DPARepository<Category, int>, ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Category> GetQueryable()
    {
        return _context.Categories;
    }
}
