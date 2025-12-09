using AutoMapper;
using DotNet_StoreManagement.Domain.entities;

namespace DotNet_StoreManagement.Features.AuthAPI.repositories;

[AutoMap(typeof(User), ReverseMap = true)]
public class UserDTO
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}