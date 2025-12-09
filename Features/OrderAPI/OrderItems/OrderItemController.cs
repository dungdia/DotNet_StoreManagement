using System.Net;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.OrderAPI.dtos;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_StoreManagement.Features.OrderAPI.OrderItems;

[ApiController]
[Route("api/v1/order-item/")]
public class OrderItemController : Controller
{
    private readonly OrderItemService _service;

    public OrderItemController(OrderItemService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderItemsAsync(
        [FromRoute] int id
    )
    {
        var result = await _service.GetOrderItemsByOrderIdAsync(id);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get order items successfully",
            result
        );

        return StatusCode(response.statusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderItemAPI(
        [FromBody] List<OrderItemDTO> dtoList
    )
    {
        var result = await _service.CreateOrderItemListAsync(dtoList);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Create order item successfully", result);
        return StatusCode(response.statusCode, result);
    }

}
