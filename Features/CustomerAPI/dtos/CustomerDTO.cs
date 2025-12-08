using AutoMapper;
using DotNet_StoreManagement.Domain.entities;

namespace DotNet_StoreManagement.Features.CustomerAPI;

[AutoMap(typeof(Customer), ReverseMap = true)]

public class CustomerDTO
{
    public string Name { get; set; } = null!;
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}
