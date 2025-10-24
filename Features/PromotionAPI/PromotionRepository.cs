using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DotNet_StoreManagement.Features.PromotionAPI
{
    [Repository]
    public class PromotionRepository : DPARepository<Promotion, int>
    {
        private readonly AppDbContext _context;

        public PromotionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Promotion, bool>> predicate)
        {
            return await _context.Set<Promotion>().AnyAsync(predicate);
        }
    }
}
