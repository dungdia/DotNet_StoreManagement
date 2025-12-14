using AutoMapper;
using CloudinaryDotNet.Actions;
using DotNet_StoreManagement.Domain.entities;

namespace DotNet_StoreManagement.Features.UserAPI.dtos
{
    [AutoMap(typeof(User), ReverseMap = true)]
    public class UserResponseDTO
    {
        public string UserId { get; set; }
        public string Username { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
