using System.Linq.Expressions;
using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.OrderAPI.dtos;
using DotNet_StoreManagement.Features.OrderAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.utils;
using Serilog;

namespace DotNet_StoreManagement.Features.OrderAPI.Orders;

[Service]
public class OrderService
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ICollection<Order>> GetOrderAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Order> CreateOrderAsync(OrderDTO dto)
    {
        var order = _mapper.Map<Order>(dto);
        
        await _repo.AddAsync(order);
        var affectedRows = await _repo.SaveChangesAsync();

        if (affectedRows <= 0)
            throw APIException.InternalServerError("Create order failed"); 
        
        return order;
    }

    public async Task<Order> UpdateOrderAsync(int id, OrderDTO dto)
    {
        var order = await _repo.GetByIdAsync(id);

        if (order == null)
            throw APIException.BadRequest("Order not found");
        await _repo.UpdateAsync(_mapper.Map(dto, order));

        var affectedRows = await _repo.SaveChangesAsync();
        if (affectedRows < 0) throw new ApplicationException("update failed");

        return order;
    }

}
