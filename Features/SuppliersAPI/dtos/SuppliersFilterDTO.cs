using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using DotNet_StoreManagement.Features.ProductAPI.dtos;

namespace DotNet_StoreManagement.Features.SuppliersAPI.dtos
{
    public class SuppliersFilterDTO : BaseFilter
    {

        public int? Id { get; set; }
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
    }

}
