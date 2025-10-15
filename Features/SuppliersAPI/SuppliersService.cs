using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.ProductAPI.impl;
using DotNet_StoreManagement.Features.SuppliersAPI.dtos;
using DotNet_StoreManagement.Features.SuppliersAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;

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

        public async Task<Supplier> addSuppilers(SuppliersDto suppliersDto)
        {
            Supplier result = _mapper.Map<Supplier>(suppliersDto);
            var affectedRows = await _repo.AddAndSaveAsync(result);

            if (affectedRows < 0) throw APIException.InternalServerError("Add failed");

            return result;
        }
    }
}
