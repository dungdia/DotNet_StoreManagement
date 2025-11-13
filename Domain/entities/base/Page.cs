namespace DotNet_StoreManagement.Domain.entities.@base;

public class Page<T>
{
    public ICollection<T> Content { get; set; } = new List<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public long TotalPages { get; set; }
    public long TotalElements { get; set; }
    public bool HasNext => PageNumber < TotalPages;
    public bool HasPrevious => PageNumber > 1;
}