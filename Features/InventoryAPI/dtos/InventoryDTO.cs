using AutoMapper;
using DotNet_StoreManagement.Domain.entities;

namespace DotNet_StoreManagement.Features.InventoryAPI.dtos;
[AutoMap(typeof(Inventory), ReverseMap = true)]
public class InventoryDTO
{
    public int InventoryId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? UpdatedAt { get; set; }
}