using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.UserAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;
using Microsoft.EntityFrameworkCore;

namespace DotNet_StoreManagement.Features.UserAPI
{
    [Repository]
    public class UserRepository : DPARepository<User, int>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                                 .FirstOrDefaultAsync(u => u.Username == username);
        }

        public IQueryable<User> GetQueryable()
        {
            return _context.Users;
        }
    }
}
