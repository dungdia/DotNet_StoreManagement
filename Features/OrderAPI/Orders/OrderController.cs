using System.Net;
using AutoMapper;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.OrderAPI.dtos;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DotNet_StoreManagement.Features.OrderAPI.Orders;

[ApiController]
[Route("api/v1/order/")]
public class OrderController : Controller
{
    private readonly OrderService _service;

    public OrderController(OrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync()
    {
        var result = await _service.GetOrderAsync();
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get orders successfully",
            result
        );

        return StatusCode(response.statusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAPI(
        [FromBody] OrderDTO dto
    )
    {
        var result = await _service.CreateOrderAsync(dto);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Create order successfully", result);

        return StatusCode(response.statusCode, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOrderAPI(
        [FromRoute] int id,
        [FromBody] OrderDTO dto
    )
    {
        var result = await _service.UpdateOrderAsync(id, dto);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Update order successfully", result);
        return StatusCode(response.statusCode, result);
    }

}
