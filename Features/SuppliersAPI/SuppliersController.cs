using System.Net;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.ProductAPI;
using DotNet_StoreManagement.Features.SuppliersAPI.dtos;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_StoreManagement.Features.SuppliersAPI;

[ApiController]
[Route("api/v1/suppliers/")]
public class SuppliersController : Controller
{
    private readonly SuppliersService _service;

    public SuppliersController(SuppliersService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var result = await _service.getAllSuppliers();

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get suppliers successfully",
            result
        );

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddSuppliers(SuppliersDto suppliersDto)
    {
        var result = await _service.addSuppilers(suppliersDto);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Add suppliers successfully",
            result
        );
        return Ok(response);
    }
}
