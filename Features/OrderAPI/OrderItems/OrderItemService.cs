using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.OrderAPI.dtos;
using DotNet_StoreManagement.Features.OrderAPI.impl;
using DotNet_StoreManagement.Features.OrderAPI.mapping;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;

namespace DotNet_StoreManagement.Features.OrderAPI.OrderItems;

[Service]
public class OrderItemService
{

    private readonly IOrderItemRepository _repo;
    private readonly IOrderItemMapper _mapper;

    public OrderItemService(IOrderItemRepository repo, IOrderItemMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<List<OrderItemDTO>> GetOrderItemsWithProductByOrderIdAsync(int orderId)
    {
        var orderItems = await _repo.FindWithProductAsync(oi => oi.OrderId == orderId);
        return orderItems.Select(orderItem => _mapper.ToDTO(orderItem)).ToList();
    }


    public async Task<OrderItem> CreateOrderItemAsync(OrderItemDTO dto)
    {
        var orderItem = _mapper.ToEntity(dto);
        await _repo.AddAsync(orderItem);
        var affectedRows = await _repo.SaveChangesAsync();

        if (affectedRows <= 0)
            throw APIException.BadRequest("Create Order Item failed");
        return orderItem;
    }

    public async Task<List<OrderItem>> CreateOrderItemListAsync(List<OrderItemDTO> dtoList)
    {
        var orderItemList = dtoList.Select(dto => _mapper.ToEntity(dto)).ToList();
        foreach (var orderItem in orderItemList)
        {
            await _repo.AddAsync(orderItem);
        }
        var affectedRows = await _repo.SaveChangesAsync();
        if (affectedRows <= 0)
            throw APIException.BadRequest("Create Order Item List failed");
        return orderItemList;
    }

}
