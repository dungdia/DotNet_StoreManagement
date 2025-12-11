using AutoMapper;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.OrderAPI.dtos;
using DotNet_StoreManagement.Features.PaymentAPI;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;
using VNPAY;
using VNPAY.Models;
using VNPAY.Models.Enums;
using VNPAY.Models.Exceptions;

namespace DotNet_StoreManagement.Features.OrderAPI.Orders;

[ApiController]
[Route("api/v1/order/")]
public class OrderController : Controller
{
    private readonly OrderService _service;
    private readonly PaymentService _paymentService;
    private readonly IVnpayClient _vnpayService;

    public OrderController(OrderService service, PaymentService paymentService, IVnpayClient vnpayService)
    {
        _service = service;
        _paymentService = paymentService;
        _vnpayService = vnpayService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync(
        [FromQuery] OrderFilterDTO filterDTO,
        [FromQuery] PageRequest pageRequest)
    {
        var result = await _service.GetPageableOrdersAsync(filterDTO, pageRequest);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get orders successfully",
            result
        );

        return StatusCode(response.statusCode, response);
    }

    [HttpGet("customer/{customerId:int}")]
    public async Task<IActionResult> GetOrderByCustomerIdAsync(
        [FromRoute] int customerId)
    {
        var result = await _service.GetOrdersByCustomerIdAsync(customerId);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get orders successfully",
            result
        );

        return StatusCode(response.statusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAPI(
        [FromBody] OrderDTO dto
    )
    {
        var result = await _service.CreateOrderAsync(dto);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Create order successfully", result);

        return StatusCode(response.statusCode, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOrderAPI(
        [FromRoute] int id,
        [FromBody] OrderStatusRequest dto
    )
    {
        var result = await _service.UpdateOrderStatusAsync(id, dto);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Update order successfully", result);
        return StatusCode(response.statusCode, response);
    }

    [HttpPost("checkout/online")]
    public async Task<IActionResult> CreateOnlineOrderAPI(
        [FromBody] OnlineOrderDTO dto
    )
    {
        var result = await _service.CreateOnlineOrderAsync(dto);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Đặt hàng thành công",
            result
        );

        return StatusCode(response.statusCode, response);
    }

    [HttpPost("checkout/banking")]
    public async Task<IActionResult> CreateBankingOrderAPI(
        [FromBody] OnlineOrderDTO dto
    )
    {
        var orderResult = await _service.CreateBankingOrderAsync(dto);
        string paymentUrl = "";
        if(dto.PaymentMethod == "bank_transfer")
        {
            var vnpayrequest = new VnpayPaymentRequest();
            vnpayrequest.Money = (double) orderResult.TotalAmount;
            vnpayrequest.Description = $"Thanh toan don hang #{orderResult.OrderId} tai DotNet Store";
            vnpayrequest.BankCode = BankCode.ANY;
            vnpayrequest.Language = DisplayLanguage.Vietnamese;

            long transactionRef = vnpayrequest.PaymentId;

            await _paymentService.UpdateTransactionRefAsync(orderResult.OrderId, transactionRef);

            paymentUrl = _vnpayService.CreatePaymentUrl(vnpayrequest).Url;

        }

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Đặt hàng thành công",
            new {
                Order = orderResult,
                PaymentUrl = paymentUrl
            }
        );
        return StatusCode(response.statusCode, response);
    }

    [HttpGet("checkout/proceedAfterPayment")]
    public async Task<IActionResult> ProceedAfterPayment()
    {
        try
        {
            var paymentResult = _vnpayService.GetPaymentResult(this.Request);

            long transactionRef = paymentResult.PaymentId;
            await _paymentService.ConfirmPaymentSuccessAsync(transactionRef);
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Thanh toán thành công",
                paymentResult
            );
            return StatusCode(response.statusCode, response);
        }
        catch (VnpayException ex)  // Bắt lỗi liên quan đến VNPAY
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
