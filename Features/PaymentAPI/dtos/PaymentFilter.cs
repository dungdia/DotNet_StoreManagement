namespace DotNet_StoreManagement.Features.PaymentAPI.dtos;

public class PaymentFilter
{
    public int? OrderId { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public DateTime? PaymentDate { get; set; }
}