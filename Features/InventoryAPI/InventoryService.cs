using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.InventoryAPI.dtos;
using DotNet_StoreManagement.Features.InventoryAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;

namespace DotNet_StoreManagement.Features.InventoryAPI
{
    [Service]
    public class InventoryService
    {
        private readonly IInventoryRepository _repo;
        private readonly IMapper _mapper;

        public InventoryService(IInventoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ICollection<Inventory>> getAllInventory()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Inventory> addInventory(InventoryDTO inventoryDto)
        {
            Inventory result = _mapper.Map<Inventory>(inventoryDto);
            var affectedRows = await _repo.AddAndSaveAsync(result);

            if (affectedRows < 0) throw APIException.InternalServerError("Add failed");

            return result;
        }
    }
}
