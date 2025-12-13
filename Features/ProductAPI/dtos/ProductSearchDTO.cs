using DotNet_StoreManagement.Domain.enums;

namespace DotNet_StoreManagement.Features.ProductAPI.dtos;

public class ProductSearchDTO
{
    public string? ProductName { get; set; }

    public string? Barcode { get; set; }

    public string? Unit { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ProductImg { get; set; }
    
    public string? Category { get; set; }

    public string? Supplier { get; set; }

    public OrderBy SortOder { get; set; }

    public Decimal? MinPrice { get; set; }
    
    public Decimal? MaxPrice { get; set; }
    
    public DateOnly? StartDate { get; set; }
    
    public DateOnly? EndDate { get; set; }
}