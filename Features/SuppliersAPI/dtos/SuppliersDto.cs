using AutoMapper;
using DotNet_StoreManagement.Domain.entities;

namespace DotNet_StoreManagement.Features.SuppliersAPI.dtos
{

    [AutoMap(typeof(Supplier),ReverseMap =true)]
    public class SuppliersDTO
    {
        public string Name { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}
