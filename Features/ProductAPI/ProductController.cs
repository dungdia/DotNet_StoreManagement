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
    
    // [HttpGet]
    // public async Task<IActionResult> GetPagesOfProductAPI(
    //     [FromQuery] PageRequest pageRequest
    // )
    // {
    //     var result = await _service.getPageableProduct(null, pageRequest);
    //
    //     var response = new APIResponse<Object>(
    //         HttpStatusCode.OK.value(),
    //         "Get product successfully",
    //         result.Content
    //     ).setMetadata(new
    //     {
    //         pageNumber = result.PageNumber,
    //         pageSize = result.PageSize,
    //         totalPages = result.TotalPages
    //     });
    //     
    //     return StatusCode(response.statusCode, response);
    // }
    
    [HttpGet]
    public async Task<IActionResult> GetPagesOfProductAPI(
        [FromQuery] ProductFilterDTO dtoFilter,
        [FromQuery] PageRequest pageRequest
    )
    {
        var result = await _service.getPageableProduct(dtoFilter, pageRequest);

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get product successfully",
            result.Content
        ).setMetadata(new
        {
            pageNumber = result.PageNumber,
            pageSize = result.PageSize,
            totalPages = result.TotalPages
        });
        
        return StatusCode(response.statusCode, response);
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadMangaAPI(
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
        
        var result = await _service.EditProduct(Int32.Parse(id), dto);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Edit product successfully",
            result
        );
        
        return StatusCode(response.statusCode, response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAPI()
    {
        var id = RouteData.Values["id"]?.ToString()!;
        
        var result = await _service.DeleteProduct(Int32.Parse(id));
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Delete product successfully",
            result
        );
        
        return StatusCode(response.statusCode, response);
    }
}