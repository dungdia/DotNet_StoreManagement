using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.CustomerAPI.dtos;
using DotNet_StoreManagement.Features.UserAPI.dtos;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static NRedisStack.Search.Query;

namespace DotNet_StoreManagement.Features.UserAPI
{
    [ApiController]
    [Route("api/v1/User/")]
    public class UserController : Controller
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(
            [FromQuery] UserFilterDTO dtoFilter,
            [FromQuery] PageRequest pageRequest
        )
        {
            var result = await _service.GetPageableUserAsync(dtoFilter, pageRequest);

            var response = new APIResponse<object>(
                (int)HttpStatusCode.OK,
                "Get users successfully",
                result.Content
            ).setMetadata(new
            {
                pageNumber = result.PageNumber,
                pageSize = result.PageSize,
                totalPages = result.TotalPages,
                totalElements = result.TotalElements
            });

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserAPI(
            [FromBody] UserDTO dto
            )
        {
            var result = await _service.CreateUserAsync(dto);

            var response = new APIResponse<Object>(
              (int)HttpStatusCode.Created,
                "Create user successfully",
                result);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUserAPI(
           int id,
           [FromBody] UpdaterUserDTO dto
        )
        {
            var result = await _service.EditUser(id, dto);

            var response = new APIResponse<UserResponseDTO?>(
                (int)HttpStatusCode.OK,
                "Update user successfully",
                result);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAPI(int id)
        {
            await _service.DeleteUser(id);

            var response = new APIResponse<object?>(
                (int)HttpStatusCode.OK,
                "Delete user successfully",
                null
            );

            return Ok(response);
        }

    }
}
