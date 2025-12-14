using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.SharedKernel.persistence.impl;
using Microsoft.EntityFrameworkCore;

namespace DotNet_StoreManagement.Features.CustomerAPI.impl
{
    public interface ICustomerRepository : IDPARepository<Customer, int>
    {
        public IQueryable<Customer> GetQueryable();
        //public Task<Customer?> GetByUserIdAsync(int userId);
    }
}
