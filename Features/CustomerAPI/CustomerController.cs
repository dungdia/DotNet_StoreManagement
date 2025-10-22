using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.CustomerAPI.dtos;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DotNet_StoreManagement.Features.CustomerAPI
{
    [ApiController]
    [Route("api/v1/Customer/")]
    public class CustomerController : Controller
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomersAsync(
            [FromQuery] PageRequest pageRequest
        ) {
            var result = await _service.GetPageableCustomerAsync(null, pageRequest);

            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Get customers successfully",
                result.Content
            ).setMetadata(new
            {
                pageNumber = result.PageNumber,
                pageSize = result.PageSize,
                totalPages = result.TotalPages
            });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAPI(
            [FromBody] CustomerDTO dto
        )
        {
            var result = await _service.CreateCustomerAsync(dto);

            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Create customer successfully",
                result);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomerAPI(
            [FromBody] CustomerDTO dto
        )
        {
            var id = RouteData.Values["id"]?.ToString()!;
            var result = await _service.EditCustomer(Int32.Parse(id), dto);
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Update customer successfully",
                result);
            
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAPI()
        {
            var id = RouteData.Values["id"]?.ToString()!;
            var result = await _service.DeleteCustomer(Int32.Parse(id));
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Delete customer successfully",
                result);
            
            return Ok(response);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchCustomersAsync(
            [FromQuery] CustomerFilterDTO dtoFilter,
            [FromQuery] PageRequest pageRequest
        ) {
            var result = await _service.GetPageableCustomerAsync(dtoFilter, pageRequest);
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Get customers successfully",
                result.Content
            ).setMetadata(new
            {
                pageNumber = result.PageNumber,
                pageSize = result.PageSize,
                totalPages = result.TotalPages
            });
            return Ok(response);
        }
    }
}
