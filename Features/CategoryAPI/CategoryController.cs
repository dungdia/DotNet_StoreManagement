using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.CategoryAPI.dtos;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;

namespace DotNet_StoreManagement.Features.CategoryAPI
{
    [ApiController]
    [Route("api/v1/Category/")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync(
                [FromQuery] PageRequest pageRequest
            )
        {
            var result = await _service.GetPageableCategoryAsync(null, pageRequest);

            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Get categories successfully",
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
        public async Task<IActionResult> CreateCategoryAPI(
                [FromBody] CategoryDTO dto
            )
        {
            var result = await _service.CreateCategoryAsync(dto);

            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Create category successfully",
                result);
            return StatusCode(response.statusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAPI(
                [FromBody] CategoryDTO dto
            )
        {
            var id = RouteData.Values["id"]?.ToString();
            var result = await _service.EditCategory(Convert.ToInt32(id), dto);
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Update category successfully",
                result
            );
            return StatusCode(response.statusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAPI()
        {
            var id = RouteData.Values["id"]?.ToString();
            var result = await _service.DeleteCategory(Convert.ToInt32(id));
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Delete category successfully",
                result
            );
            return StatusCode(response.statusCode, response);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchCategoriesAPI(
                [FromQuery] CategoryFilterDTO filterDTO,
                [FromQuery] PageRequest pageRequest
            )
        {
            var result = await _service.GetPageableCategoryAsync(filterDTO, pageRequest);
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Search categories successfully",
                result.Content
            ).setMetadata(new
            {
                pageNumber = result.PageNumber,
                pageSize = result.PageSize,
                totalPages = result.TotalPages
            });
            return StatusCode(response.statusCode, response);
        }
    }
}
