using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using DotNet_StoreManagement.Features.OrderAPI.dtos;
using DotNet_StoreManagement.Features.OrderAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.persistence;
using DotNet_StoreManagement.SharedKernel.utils;
using Serilog;
using System.Linq.Expressions;

namespace DotNet_StoreManagement.Features.OrderAPI.Orders;

[Service]
public class OrderService
{
    private readonly IOrderRepository _repo;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repo, IMapper mapper, AppDbContext context)
    {
        _repo = repo;
        _mapper = mapper;
        _context = context;
    }

    public async Task<ICollection<Order>> GetOrderAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Page<Order>> GetPageableOrdersAsync(OrderFilterDTO? filterDTO, PageRequest pageRequest)
    {
        IQueryable<Order> query = _repo.GetQueryable();

        query = query
            .RangeValue("TotalAmount", filterDTO?.MinPrice, filterDTO?.MaxPrice)
            .FilterByList<Order, int>("CustomerId", filterDTO.CustomerIds)
            .FilterByList<Order, int>("UserId", filterDTO.UserIds)
            .ApplyOrdering(filterDTO.SortBy, filterDTO.SortDescending);

        return await _repo.FindAllPageAsync(
            query,
            pageRequest.PageNumber,
            pageRequest.PageSize
        );
    }

    public async Task<ICollection<Order>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _repo.GetByFKIdAsync("CustomerId", customerId);
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

    public async Task<Order> CreateOnlineOrderAsync(OnlineOrderDTO dto)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var newOrderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId)
                    ?? throw new Exception($"Sản phẩm {item.ProductId} không tồn tại");
                //TODO: Check tồn kho
                var subtotal = product.Price * item.Quantity;
                newOrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price,
                    Subtotal = subtotal
                });
                totalAmount += subtotal;

            }

            var newOrder = new Order
            {
                CustomerId = dto.CustomerId,
                UserId = dto.UserId,
                PromoId = null, //TODO: handle promo code
                Status = "Pending",
                TotalAmount = totalAmount,
                DiscountAmount = 0,
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            foreach (var item in newOrderItems)
            {
                item.OrderId = newOrder.OrderId;
                _context.OrderItems.Add(item);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return newOrder;
        }
        catch (Exception ex)
        {
            Log.Error("Error creating online order: {Message}", ex.Message);
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Order> CreateBankingOrderAsync(OnlineOrderDTO dto)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var newOrderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId)
                    ?? throw new Exception($"Sản phẩm {item.ProductId} không tồn tại");
                //TODO: Check tồn kho
                var subtotal = product.Price * item.Quantity;
                newOrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price,
                    Subtotal = subtotal
                });
                totalAmount += subtotal;

            }

            var newOrder = new Order
            {
                CustomerId = dto.CustomerId,
                UserId = dto.UserId,
                PromoId = null, //TODO: handle promo code
                Status = "Pending",
                TotalAmount = totalAmount,
                DiscountAmount = 0,
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            foreach (var item in newOrderItems)
            {
                item.OrderId = newOrder.OrderId;
                _context.OrderItems.Add(item);
            }

            var newPayment = new Payment
            {
                OrderId = newOrder.OrderId,
                Amount = totalAmount,
                PaymentMethod = "bank_transfer",
                PaymentDate = DateTime.UtcNow,
                Status = PaymentStatus.PENDING.ToString() //TODO : handle payment status
            };

            _context.Payments.Add(newPayment);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return newOrder;
        }
        catch (Exception ex)
        {
            Log.Error("Error creating online order: {Message}", ex.Message);
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Order> UpdateOrderStatusAsync(int id, OrderStatusRequest dto)
    {
        var order = await _repo.GetByIdAsync(id);

        if (order == null)
            throw APIException.BadRequest("Order not found");
        await _repo.UpdateAsync(_mapper.Map(dto, order));

        var affectedRows = await _repo.SaveChangesAsync();
        if (affectedRows < 0) throw new ApplicationException("update failed");

        return order;
    }

    public async Task<OrderDTO?> GetOrderByIdAsync(int id)
    {
        var order = await _repo.GetByIdAsync(id);
        if (order == null) throw APIException.BadRequest("Invalid Customer's ID");
        return _mapper.Map<OrderDTO>(order);
    }
}
