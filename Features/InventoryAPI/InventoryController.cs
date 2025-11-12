using System.Net;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.InventoryAPI.dtos;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_StoreManagement.Features.InventoryAPI;

[ApiController]
[Route("api/v1/inventory/")]
public class InventoryController : Controller
{
    private readonly InventoryService _service;

    public InventoryController(InventoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInventory()
    {
        var result = await _service.getAllInventory();

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get all Inventories successfully",
            result
        );

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddSuppliers(InventoryDTO inventoryDto)
    {
        var result = await _service.addInventory(inventoryDto);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Add new inventory successfully",
            result
        );
        return Ok(response);
    }
}
