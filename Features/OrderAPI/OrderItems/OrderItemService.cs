using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.OrderAPI.dtos;
using DotNet_StoreManagement.Features.OrderAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;

namespace DotNet_StoreManagement.Features.OrderAPI.OrderItems;

[Service]
public class OrderItemService
{

    private readonly IOrderItemRepository _repo;
    private readonly IMapper _mapper;

    public OrderItemService(IOrderItemRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ICollection<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        return await _repo.FindAsync(oi => oi.OrderId == orderId);
    }

    public async Task<OrderItem> CreateOrderItemAsync(OrderItemDTO dto)
    {
        var orderItem = _mapper.Map<OrderItem>(dto);
        await _repo.AddAsync(orderItem);
        var affectedRows = await _repo.SaveChangesAsync();

        if (affectedRows <= 0)
            throw APIException.BadRequest("Create Order Item failed");
        return orderItem;
    }

    public async Task<List<OrderItem>> CreateOrderItemListAsync(List<OrderItemDTO> dtoList)
    {
        var orderItemList = _mapper.Map<List<OrderItem>>(dtoList);
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
