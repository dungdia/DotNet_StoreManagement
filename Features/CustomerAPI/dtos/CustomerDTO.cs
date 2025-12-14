using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using System.ComponentModel.DataAnnotations;

namespace DotNet_StoreManagement.Features.CustomerAPI;

[AutoMap(typeof(Customer), ReverseMap = true)]

public class CustomerDTO
{
    [MinLength(3, ErrorMessage = "Tên khách hàng phải có ít nhất 3 ký tự.")]
    public string Name { get; set; } = null!;
    [RegularExpression(@"^\d{9,11}$", ErrorMessage = "Số điện thoại phải bao gồm 9 đến 11 số")]
    public string? Phone { get; set; } = null;
    [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
    public string? Email { get; set; } = null;
    public string? Address { get; set; } = null;
    public int? AccountId { get; set; } = null;
}
