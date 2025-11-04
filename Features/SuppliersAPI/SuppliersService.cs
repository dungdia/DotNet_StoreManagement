using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using DotNet_StoreManagement.Features.ProductAPI.dtos;
using DotNet_StoreManagement.Features.ProductAPI.impl;
using DotNet_StoreManagement.Features.SuppliersAPI.dtos;
using DotNet_StoreManagement.Features.SuppliersAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.persistence;

namespace DotNet_StoreManagement.Features.SuppliersAPI
{
    [Service]
    public class SuppliersService
    {
        private readonly ISuppliersRepository _repo;
        private readonly IMapper _mapper;

        public SuppliersService(ISuppliersRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ICollection<Supplier>> getAllSuppliers()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Supplier?> getOneSuppliers(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Page<Supplier>> searchSuppliers(SuppliersFilterDTO? filterDto, PageRequest pageRequest)
        {
            IQueryable<Supplier> query = _repo.GetQueryable();

            query = query
                .Filter("SupplierId", filterDto?.Id.ToString(), FilterType.EQUAL)
                .Filter("Name", filterDto?.Name, FilterType.CONTAINS)
                .Filter("Phone", filterDto?.Phone, FilterType.CONTAINS)
                .Filter("Email", filterDto?.Email, FilterType.CONTAINS)
                .Filter("Address", filterDto?.Address, FilterType.CONTAINS);

            return await _repo.FindAllPageAsync(
                query,
                pageRequest.PageNumber,
                pageRequest.PageSize
            );
        }

        public async Task<Supplier> addSuppilers(SuppliersDTO suppliersDto)
        {
            Supplier result = _mapper.Map<Supplier>(suppliersDto);
            var affectedRows = await _repo.AddAndSaveAsync(result);

            if (affectedRows < 0) throw APIException.InternalServerError("Add failed");

            return result;
        }

        public async Task<Supplier> updateSuppliers(int id,SuppliersDTO suppliersDto)
        {
            var result = _mapper.Map<Supplier>(suppliersDto);
            result.SupplierId = id;

            var affectedRows = await _repo.UpdateAndSaveAsync(result);

            if (affectedRows < 0) throw APIException.InternalServerError("Update failed");

            return result;
        }
    }
}
