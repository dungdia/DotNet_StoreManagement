using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.SharedKernel.persistence.impl;

namespace DotNet_StoreManagement.Features.CustomerAPI.impl
{
    public interface ICustomerRepository : IDPARepository<Customer, int>
    {
    }
}
