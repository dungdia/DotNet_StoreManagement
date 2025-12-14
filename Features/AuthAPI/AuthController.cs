using System.Net;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.AuthAPI.repositories;
using DotNet_StoreManagement.SharedKernel.security;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_StoreManagement.Features.AuthAPI;

[ApiController]
[Route("/api/v1/")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public AuthController(
        AuthService authService
    )
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> registerAccountAPI(
        [FromBody] UserDTO request
    )
    {
        var result = await _authService.register(request);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Login successfully",
            result
        );
        
        return StatusCode(response.statusCode, response);
    }

    [HttpPost("user-register")]
    public async Task<IActionResult> registerUserAccountAPI(
        [FromBody] UserDTO request
    )
    {
        var result = await _authService.register(request);

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Login successfully",
            result
        );

        return StatusCode(response.statusCode, response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> loginAccountAPI(
        [FromBody] UserDTO request
    )
    {
        var result = await _authService.authenticate(request);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Login successfully",
            result
        );

        return StatusCode(response.statusCode, response);
    }
    
    [HttpGet("auth/{url}")]
    [PreAuthorize("hasRole('staff')")]
    public ActionResult StaffDashboard(String url)
    {
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            $"Welcome {url}",
            url
        );
    
        return StatusCode(response.statusCode, response);
    }

    [HttpGet("auth/token_payload")]
    public async Task<IActionResult> getALlClaim(
        [FromHeader(Name = "Authorization")] String authorization
    )
    {
        var token = authorization.Replace("Bearer ", "");
        var result = await _authService.getAllClaim(token);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Token is valid",
            result?.Payload
        );
        return StatusCode(response.statusCode, response);
    }

    // [HttpGet("auth/secure-by-permission")]
    // [Permission("Export")]
    // public ActionResult<Object> exportPDF()
    // {
    //     return "PDF is ok";
    // }
}