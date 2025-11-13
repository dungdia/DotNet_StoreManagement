using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.SharedKernel.persistence.impl;

namespace DotNet_StoreManagement.Features.CategoryAPI.impl
{
    public interface ICategoryRepository : IDPARepository<Category, int>
    {
        public IQueryable<Category> GetQueryable();
    }
}
