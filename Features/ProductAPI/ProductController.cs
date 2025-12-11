using System.Net;
using AutoMapper;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.ProductAPI.dtos;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DotNet_StoreManagement.Features.ProductAPI;

[ApiController]
[Route("api/v1/product/")]
public class ProductController : Controller
{
    private readonly ProductService _service;
    
    public ProductController(ProductService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPagesOfProductAPI(
        [FromQuery] ProductFilterDTO dtoFilter,
        [FromQuery] PageRequest pageRequest
    )
    {
        var result = await _service.GetPageableProduct(dtoFilter, pageRequest);

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get product successfully",
            result.Content
        ).setMetadata(new
        {
            pageNumber = result.PageNumber,
            pageSize = result.PageSize,
            totalPages = result.TotalPages,
            totalElements = result.TotalElements
        });
        
        return StatusCode(response.statusCode, response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductDetailAPI(
        [FromRoute] int id    
    )
    {
        var result = await _service.getProductDetail(id);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get product successfully",
            result!
        );
        
        return StatusCode(response.statusCode, response);
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadProductAPI(
        [FromBody] ProductDTO dto    
    )
    {
        var result = await _service.UploadProduct(dto);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Upload product successfully",
            result
        );
        
        return StatusCode(response.statusCode, response);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> EditProductAPI(
        [FromBody] ProductDTO dto    
    )
    {
        var id = RouteData.Values["id"]?.ToString()!;
        
        var result = await _service.editProduct(Int32.Parse(id), dto);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Edit product successfully",
            result!
        );
        
        return StatusCode(response.statusCode, response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAPI(
        [FromRoute] int id     
    )
    {
        var result = await _service.deleteProduct(id);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Delete product successfully",
            result
        );
        
        return StatusCode(response.statusCode, response);
    }
}