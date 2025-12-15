using AutoMapper;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.OrderAPI.dtos;
using DotNet_StoreManagement.Features.OrderAPI.OrderItems;
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
    private readonly OrderItemService _orderItemService;
    private readonly IVnpayClient _vnpayService;

    public OrderController(OrderService service, PaymentService paymentService, IVnpayClient vnpayService, OrderItemService orderItemService)
    {
        _service = service;
        _paymentService = paymentService;
        _vnpayService = vnpayService;
        _orderItemService = orderItemService;
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
            result.Content
        ).setMetadata(new
        {
            pageNumber = result.PageNumber,
            pageSize = result.PageSize,
            totalPages = result.TotalPages,
            totalElements = result.TotalElements
        }
        ) ;

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
            var vnpayrequest = new VnpayPaymentRequest
                
                ();
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

    [HttpGet("checkout/repayOrder/{orderId:int}")]
    public async Task<IActionResult> RepayOrderAPI(int orderId)
    {
        string paymentUrl = "";
        var repayResult = await _service.RepayOrderAsync(orderId);

        var vnpayrequest = new VnpayPaymentRequest();
        vnpayrequest.Money = (double)repayResult.TotalAmount;
        vnpayrequest.Description = $"Thanh toan don hang #{repayResult.OrderId} tai DotNet Store";
        vnpayrequest.BankCode = BankCode.ANY;
        vnpayrequest.Language = DisplayLanguage.Vietnamese;

        long transactionRef = vnpayrequest.PaymentId;

        await _paymentService.UpdateTransactionRefAsync(repayResult.OrderId, transactionRef);

        paymentUrl = _vnpayService.CreatePaymentUrl(vnpayrequest).Url;
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Tạo URL thanh toán thành công",
            new { PaymentUrl = paymentUrl }
        );

        return StatusCode(response.statusCode, response);
    }

    [HttpGet("checkout/proceedAfterPayment")]
    public async Task<IActionResult> ProceedAfterPayment(
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
        VnpayPaymentResult paymentResult = null;
        try
        {
            paymentResult = _vnpayService.GetPaymentResult(Request.Query);
            long transactionRef = paymentResult.PaymentId;
            if(vnpResponseCode == "00")
            {
                await _paymentService.ConfirmPaymentSuccessAsync(transactionRef);
            }
                
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Thanh toán thành công",
                paymentResult
            );
            return StatusCode(response.statusCode, response);
        }
        catch (VnpayException ex)  // Bắt lỗi liên quan đến VNPAY
        {
            // 1. Dùng biến vnpTxnRef từ tham số hàm (đã có sẵn)
            if (!string.IsNullOrEmpty(vnpTxnRef) && long.TryParse(vnpTxnRef, out long transactionRef))
            {
                // Cập nhật trạng thái đơn hàng thành thất bại
                await _paymentService.ConfirmPaymentFailedAsync(transactionRef);
            }
            else
            {
                // Log lỗi: Không lấy được mã đơn hàng từ URL
                Console.WriteLine("Lỗi: Không tìm thấy vnp_TxnRef trong URL khi xử lý exception");
            }

            // 2. Trả về response (Lưu ý: paymentResult lúc này là null, đừng truyền nó vào response nếu response không handle null)
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(), // Hoặc BadRequest tùy bạn
                $"Thanh toán thất bại. Lỗi: {ex.Message}",
                null // Truyền null vì không có object kết quả
            );

            return StatusCode(response.statusCode, response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetailById()
    {
        var id = RouteData.Values["id"]?.ToString()!;
        var order = await _service.GetOrderByIdAsync(Int32.Parse(id));
        var orderItems = await _orderItemService.GetOrderItemsWithProductByOrderIdAsync(Int32.Parse(id));
        var result = new OrderDetailDTO(order!, orderItems);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get customer successfully",
            result);
        return StatusCode(response.statusCode, response);
    }
}
