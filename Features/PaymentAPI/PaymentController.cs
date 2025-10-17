using System.Net;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.PaymentAPI.dtos;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_StoreManagement.Features.PaymentAPI;

[ApiController]
[Route("api/v1/payment/")]
public class PaymentController : Controller
{
    private readonly PaymentService _service;

    public PaymentController(PaymentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetPagesOfPaymentAPI(
        [FromQuery] PageRequest pageRequest
    )
    {
        var result = await _service.GetPageablePayments(null, pageRequest);

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get payments successfully",
            result.Content
        ).setMetadata(new
        {
            pageNumber = result.PageNumber,
            pageSize = result.PageSize,
            totalPages = result.TotalPages
        });

        return Ok(response);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchPagesOfPaymentAPI(
        [FromQuery] PaymentFilter dtoFilter,
        [FromQuery] PageRequest pageRequest
    )
    {
        var result = await _service.GetPageablePayments(dtoFilter, pageRequest);

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get payments successfully",
            result.Content
        ).setMetadata(new
        {
            pageNumber = result.PageNumber,
            pageSize = result.PageSize,
            totalPages = result.TotalPages
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentByIdAPI()
    {
        var id = RouteData.Values["id"]?.ToString()!;
        var result = await _service.GetPaymentById(Int32.Parse(id));

        if (result == null)
        {
            return NotFound(new APIResponse<Object>(
                HttpStatusCode.NotFound.value(),
                "Payment not found",
                null
            ));
        }

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get payment successfully",
            result
        );

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddPaymentAPI(
        [FromBody] PaymentDTO dto
    )
    {
        var result = await _service.AddPayment(dto);

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Add payment successfully",
            result
        );

        return Ok(response);
    }
}