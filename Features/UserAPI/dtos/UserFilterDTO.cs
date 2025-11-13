using DotNet_StoreManagement.Domain.entities.@base;

namespace DotNet_StoreManagement.Features.UserAPI.dtos
{
    public class UserFilterDTO : BaseFilter
    {
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
