using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.PaymentAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.persistence;

namespace DotNet_StoreManagement.Features.PaymentAPI;

[Repository]
public class PaymentRepository : DPARepository<Payment, int>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context)
    {
    }
}