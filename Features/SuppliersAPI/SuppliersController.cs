using System.Net;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.ProductAPI;
using DotNet_StoreManagement.Features.SuppliersAPI.dtos;
using DotNet_StoreManagement.SharedKernel.exception;
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

        return StatusCode(response.statusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getSuppliersById()
    {
        int id;
        var parseResult = Int32.TryParse(RouteData.Values["id"]?.ToString()!, out id);

        if (!parseResult) throw APIException.BadRequest("id need to be number");

        var result = await _service.getOneSuppliers(id);
        if (result == null) throw APIException.BadRequest("Supplier not found");

        var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Get suppliers successfully",
                result
            );

        return StatusCode(response.statusCode, response);
    }

    [HttpGet("/search")]
    public async Task<IActionResult> searchSuppliers([FromQuery] SuppliersFilterDTO suppliersFilterDTO,[FromQuery]PageRequest pageRequest)
    {
        var allSuppliers = await _service.searchSuppliers(suppliersFilterDTO, pageRequest);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Search suppliers successfully",
            allSuppliers.Content
        ).setMetadata(new
        {
            pageNumber = allSuppliers.PageNumber,
            pageSize = allSuppliers.PageSize,
            totalPages = allSuppliers.TotalPages
        });

        return StatusCode(response.statusCode,response);
    }


    [HttpPost]
    public async Task<IActionResult> AddSuppliers(SuppliersDTO suppliersDto)
    {
        var result = await _service.addSuppilers(suppliersDto);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Add suppliers successfully",
            result
        );
        return StatusCode(response.statusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> updateSuppliers(SuppliersDTO suppliersDto)
    {
        int id;
        var parseResult = Int32.TryParse(RouteData.Values["id"]?.ToString()!, out id);

        if (!parseResult) throw APIException.BadRequest("id need to be number");

        var result = await _service.updateSuppliers(id, suppliersDto);

        var response = new APIResponse<Object>(HttpStatusCode.OK.value(),
            "Update suppliers successfully",
            result
            );

        return StatusCode(response.statusCode, response);
    }
}
