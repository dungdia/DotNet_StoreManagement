using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.SharedKernel.persistence;
using DotNet_StoreManagement.SharedKernel.persistence.impl;
using System.Linq.Expressions;

namespace DotNet_StoreManagement.Features.OrderAPI.impl;

public interface IOrderItemRepository : IDPARepository<OrderItem, int>
{
    Task<ICollection<OrderItem>> FindWithProductAsync(Expression<Func<OrderItem, bool>> predicate);
}

