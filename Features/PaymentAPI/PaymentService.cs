using System.Linq.Expressions;
using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Features.PaymentAPI.dtos;
using DotNet_StoreManagement.Features.PaymentAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.utils;

namespace DotNet_StoreManagement.Features.PaymentAPI;

[Service]
public class PaymentService
{
    private readonly IPaymentRepository _repo;
    private readonly IMapper _mapper;

    public PaymentService(IPaymentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ICollection<Payment>> GetAllPayments()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Page<Payment>> GetPageablePayments(PaymentFilter? dtoFilter, PageRequest pageRequest)
    {
        Expression<Func<Payment, bool>> filter = p => true;

        if (dtoFilter != null)
        {
            if (dtoFilter.OrderId.HasValue)
            {
                filter = p => p.OrderId == dtoFilter.OrderId.Value;
            }

            if (!string.IsNullOrEmpty(dtoFilter.PaymentMethod))
            {
                filter = p => p.PaymentMethod != null && 
                             p.PaymentMethod.ToLower().Contains(dtoFilter.PaymentMethod.ToLower());
            }

            if (dtoFilter.MinAmount.HasValue)
            {
                var previousFilter = filter;
                filter = p => previousFilter.Compile()(p) && p.Amount >= dtoFilter.MinAmount.Value;
            }

            if (dtoFilter.MaxAmount.HasValue)
            {
                var previousFilter = filter;
                filter = p => previousFilter.Compile()(p) && p.Amount <= dtoFilter.MaxAmount.Value;
            }

            if (dtoFilter.PaymentDate.HasValue)
            {
                var date = dtoFilter.PaymentDate.Value.Date;
                var previousFilter = filter;
                filter = p => previousFilter.Compile()(p) && 
                             p.PaymentDate.HasValue && 
                             p.PaymentDate.Value.Date == date;
            }
        }

        return await _repo.FindAllPageAsync(
            filter,
            null,
            pageRequest.PageNumber,
            pageRequest.PageSize
        );
    }

    public async Task<Payment> AddPayment(PaymentDTO dto)
    {
        var payment = _mapper.Map<Payment>(dto);
        payment.PaymentDate = DateTime.Now;

        await _repo.AddAsync(payment);
        var affectedRows = await _repo.SaveChangesAsync();

        if (affectedRows < 0) 
            throw APIException.InternalServerError("Add payment failed");

        return payment;
    }

    // public async Task<Payment?> EditPayment(int id, PaymentDTO dto)
    // {
    //     var payment = await _repo.GetByIdAsync(id);
    //
    //     if (payment == null) 
    //         throw APIException.BadRequest("Can't find payment");
    //
    //     await _repo.UpdateAsync(_mapper.Map(dto, payment));
    //     var affectedRows = await _repo.SaveChangesAsync();
    //
    //     if (affectedRows < 0) 
    //         throw APIException.InternalServerError("Update payment failed");
    //
    //     return payment;
    // }
    

    public async Task<Payment?> GetPaymentById(int id)
    {
        return await _repo.GetByIdAsync(id);
    }
}