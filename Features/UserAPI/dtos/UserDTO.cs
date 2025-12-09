using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.enums;

namespace DotNet_StoreManagement.Features.UserAPI.dtos
{
    [AutoMap(typeof(User), ReverseMap = true)]
    public class UserDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FullName { get; set; }
        public Role Role { get; set; }
    }
}
