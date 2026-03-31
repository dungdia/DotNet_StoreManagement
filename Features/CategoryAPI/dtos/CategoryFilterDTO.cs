using DotNet_StoreManagement.Domain.entities.@base;

namespace DotNet_StoreManagement.Features.CategoryAPI.dtos
{
    public class CategoryFilterDTO : BaseFilter
    {
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
