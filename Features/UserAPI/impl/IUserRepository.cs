using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.SharedKernel.persistence.impl;

namespace DotNet_StoreManagement.Features.UserAPI.impl
{
    public interface IUserRepository : IDPARepository<User, int>
    {
        Task<User?> GetByUsernameAsync(string username);
        public IQueryable<User> GetQueryable();
    }
}
