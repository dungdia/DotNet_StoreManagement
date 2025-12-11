using System.Net;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.PaymentAPI.dtos;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;
using VNPAY.Models;

namespace DotNet_StoreManagement.Features.PaymentAPI;

[ApiController]
[Route("api/v1/payment/")]
public class PaymentController : Controller
{
    private readonly ILogger<PaymentController> _logger;
    private readonly PaymentService _service;

    public PaymentController(PaymentService service,ILogger<PaymentController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetPagesOfPaymentAPI(
        [FromQuery] PaymentFilterDTO? dtoFilter,
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

        return StatusCode(response.statusCode, response);
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

        return StatusCode(response.statusCode, response);
    }

    [HttpPost("vnpay-payment")]
    public IActionResult createPaymentAPI([FromBody] VnpayPaymentRequest request)
    {
        var result = _service.CreatePaymentURL(request);

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Tạo thanh toán thành công",
            result  
        );

        return StatusCode(response.statusCode, response);
    }

    [HttpGet("vnpay-callback")]
    public IActionResult CallbackPaymentAPI(
        [FromQuery(Name = "vnp_Amount")] long? vnpAmount,
        [FromQuery(Name = "vnp_BankCode")] string? vnpBankCode,
        [FromQuery(Name = "vnp_BankTranNo")] string? vnpBankTranNo,
        [FromQuery(Name = "vnp_CardType")] string? vnpCardType,
        [FromQuery(Name = "vnp_OrderInfo")] string? vnpOrderInfo,
        [FromQuery(Name = "vnp_PayDate")] string? vnpPayDate,
        [FromQuery(Name = "vnp_ResponseCode")] string? vnpResponseCode,
        [FromQuery(Name = "vnp_TmnCode")] string? vnpTmnCode,
        [FromQuery(Name = "vnp_TransactionNo")] string? vnpTransactionNo,
        [FromQuery(Name = "vnp_TransactionStatus")] string? vnpTransactionStatus,
        [FromQuery(Name = "vnp_TxnRef")] string? vnpTxnRef,
        [FromQuery(Name = "vnp_SecureHash")] string? vnpSecureHash
    )
    {
        var result = _service.callbackPayment(this.Request);
        
        _logger.LogInformation("Payment ID: {PaymentId}", result.PaymentId);
        _logger.LogInformation("VNPAY Transaction ID: {VnpayTransactionId}", result.VnpayTransactionId);
        _logger.LogInformation("Timestamp: {Timestamp}", result.Timestamp);
        _logger.LogInformation("Card Type: {CardType}", result.CardType);
        _logger.LogInformation("Banking Info: {BankingInfo}", result.BankingInfor != null ? $"{result.BankingInfor.BankCode} - {result.BankingInfor.BankTransactionId}" : "N/A");

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "",
            result
        );
        
        return StatusCode(response.statusCode, response);
    }
}