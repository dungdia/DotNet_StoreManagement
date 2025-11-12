using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.SharedKernel.persistence.impl;

namespace DotNet_StoreManagement.Features.PaymentAPI.impl;

public interface IPaymentRepository : IDPARepository<Payment, int>
{
    IQueryable<Payment> GetQueryable();
}