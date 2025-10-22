using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.CustomerAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;

namespace DotNet_StoreManagement.Features.CustomerAPI;
    [Repository]
    public class CustomerRepository : DPARepository<Customer, int>, ICustomerRepository
    {
        private readonly AppDbContext _context;
        
        public CustomerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Customer> GetQueryable()
        {
            return _context.Customers;
        }
}
