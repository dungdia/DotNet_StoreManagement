namespace DotNet_StoreManagement.Domain.entities.@base;

public partial class PageRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}