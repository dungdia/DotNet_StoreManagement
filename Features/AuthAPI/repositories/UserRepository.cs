using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;
using Microsoft.EntityFrameworkCore;

namespace DotNet_StoreManagement.Features.AuthAPI.repositories;

[Repository]
public class UserRepository : DPARepository<User, int>
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> FindUserByUsername(String username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}