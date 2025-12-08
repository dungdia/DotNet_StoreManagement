using System.ComponentModel.DataAnnotations;

namespace DotNet_StoreManagement.Features.PaymentAPI.dtos;

public class VNPayRequestDTO
{
    [Required]
    private String OrderId { get; set; } = "";

    [Required] 
    private decimal Amount { get; set; }

    [Required]
    public string OrderDescription { get; set; } = "";
    
    [Required]
    public string ClientIp { get; set; } = "";
    
    public string? BankCode { get; set; }
    public string? Locale { get; set; } = "vn";
}