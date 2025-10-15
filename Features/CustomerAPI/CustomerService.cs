using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.CustomerAPI.dtos;
using DotNet_StoreManagement.Features.CustomerAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using System.Linq.Expressions;

namespace DotNet_StoreManagement.Features.CustomerAPI;
    [Service]    
public class CustomerService
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Page<Customer>> GetPageableCustomerAsync(CustomerFilter? dtoFilter, PageRequest pageRequest)
    {
        Expression<Func<Customer, bool>> filter = c => true;

        if (dtoFilter != null)
        {
            if (!string.IsNullOrEmpty(dtoFilter.CustomerId.ToString())){
                filter = (c => c.CustomerId == dtoFilter.CustomerId);
            }

            if (!string.IsNullOrEmpty(dtoFilter.Name))
            {
                filter = (c => c.Name.ToLower().Contains(dtoFilter.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(dtoFilter.Address))
            {
                filter = (c => c.Address.ToLower().Contains(dtoFilter.Address.ToLower()));
            }

            if (!string.IsNullOrEmpty(dtoFilter.Phone))
            {
                filter = (c => c.Phone != null && c.Phone.ToLower().Contains(dtoFilter.Phone.ToLower()));
            }

            if (!string.IsNullOrEmpty(dtoFilter.Email))
            {
                filter = (c => c.Email != null && c.Email.ToLower().Contains(dtoFilter.Email.ToLower()));
            }

            if (dtoFilter.CreatedAt.HasValue)
            {
                var date = dtoFilter.CreatedAt.Value.Date;
                filter = (c => c.CreatedAt.HasValue && c.CreatedAt.Value.Date == date);
            }
        }

        return await _repo.FindAllPageAsync(
            filter,
            null,
            pageRequest.PageNumber,
            pageRequest.PageSize);
    }

    public async Task<Customer> CreateCustomerAsync(CustomerDTO dto)
    {
        var customer = _mapper.Map<Customer>(dto);
        customer.CreatedAt = DateTime.Now;

        await _repo.AddAsync(customer);
        var affectedRows = await _repo.SaveChangesAsync();

        if (affectedRows == 0) throw APIException.InternalServerError("Create customer failed");

        return customer;
    }

    public async Task<Customer?> EditCustomer(int id, CustomerDTO dto)
    {
        var customer = await _repo.GetByIdAsync(id);

        if (customer == null) throw APIException.BadRequest("Invalid Customer's ID");
        await _repo.UpdateAsync(_mapper.Map(dto, customer));
        var affectedRows = await _repo.SaveChangesAsync();

        if (affectedRows == 0) throw APIException.InternalServerError("Update customer failed");
        
        return customer;
    }

    public async Task<Customer> DeleteCustomer(int id)
    {
        var customer = await _repo.GetByIdAsync(id);

        if (customer == null) throw APIException.BadRequest("Invalid Customer's ID");
        await _repo.DeleteAsync(customer);
        var affectedRows = await _repo.SaveChangesAsync();

        if (affectedRows == 0) throw APIException.InternalServerError("Delete customer failed");
        
        return customer;
    }
}
