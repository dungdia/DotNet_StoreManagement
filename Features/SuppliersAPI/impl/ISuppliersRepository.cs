
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.SharedKernel.persistence.impl;

namespace DotNet_StoreManagement.Features.SuppliersAPI.impl;

public interface ISuppliersRepository : IDPARepository<Supplier, int>
{
    public IQueryable<Supplier> GetQueryable();
}

