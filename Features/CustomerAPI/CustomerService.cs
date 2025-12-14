using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using DotNet_StoreManagement.Features.CustomerAPI.dtos;
using DotNet_StoreManagement.Features.CustomerAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using System.Linq.Expressions;
using DotNet_StoreManagement.SharedKernel.persistence;

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

    public async Task<Page<Customer>> GetPageableCustomerAsync(CustomerFilterDTO? dtoFilter, PageRequest pageRequest)
    {
        IQueryable<Customer> query = _repo.GetQueryable();
        
        query = query
            .Filter("Id", dtoFilter?.CustomerId.ToString(), FilterType.CONTAINS)
            .Filter("Name", dtoFilter?.Name, FilterType.CONTAINS)
            .Filter("Phone", dtoFilter?.Phone, FilterType.CONTAINS)
            .Filter("Email", dtoFilter?.Email, FilterType.CONTAINS)
            .Filter("Address", dtoFilter?.Address, FilterType.CONTAINS);
        
        return await _repo.FindAllPageAsync(
            query,
            pageRequest.PageNumber,
            pageRequest.PageSize
        );
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

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customer = await _repo.GetByIdAsync(id);
        if (customer == null) throw APIException.BadRequest("Invalid Customer's ID");
        return customer;
    }
}
