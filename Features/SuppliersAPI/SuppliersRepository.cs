using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.SuppliersAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;

namespace DotNet_StoreManagement.Features.SuppliersAPI
{
    [Repository]
    public class SuppliersRepository : DPARepository<Supplier,int>, ISuppliersRepository
    {
        private readonly AppDbContext _context;

        public SuppliersRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Supplier> GetQueryable()
        {
            return _context.Suppliers;
        }
    }
}
