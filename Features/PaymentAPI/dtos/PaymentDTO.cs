using AutoMapper;
using DotNet_StoreManagement.Domain.entities;


namespace DotNet_StoreManagement.Features.PaymentAPI.dtos;

[AutoMap(typeof(Payment), ReverseMap = true)]
public class PaymentDTO
{

    public decimal Amount { get; set; }
    
    public string? PaymentMethod { get; set; }

    public DateTime? PaymentDate { get; set; }
}