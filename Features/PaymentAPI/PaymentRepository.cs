using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.PaymentAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;

namespace DotNet_StoreManagement.Features.PaymentAPI;

[Repository]
public class PaymentRepository : DPARepository<Payment, int>, IPaymentRepository
{
    private readonly AppDbContext _context;
    public PaymentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Payment> GetQueryable()
    {
        return _context.Payments;
    }
}