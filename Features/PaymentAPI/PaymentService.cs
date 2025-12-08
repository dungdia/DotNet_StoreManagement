using System.Linq.Expressions;
using AutoMapper;
using CloudinaryDotNet;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using DotNet_StoreManagement.Features.PaymentAPI.dtos;
using DotNet_StoreManagement.Features.PaymentAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.persistence;
using DotNet_StoreManagement.SharedKernel.utils;
using VNPAY;
using VNPAY.Models;
using VNPAY.Models.Exceptions;

namespace DotNet_StoreManagement.Features.PaymentAPI;

[Service]
public class PaymentService
{
    private readonly IPaymentRepository _repo;
    private readonly IMapper _mapper;
    private IConfiguration _configuration;
    private IVnpayClient vnpayClient;
    
    public PaymentService(IPaymentRepository repo, IMapper mapper, IVnpayClient vnpayClient)
    {
        _repo = repo;
        _mapper = mapper;
        this.vnpayClient = vnpayClient;
    }

    public async Task<ICollection<Payment>> GetAllPayments()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Page<Payment>> GetPageablePayments(PaymentFilterDTO? dtoFilter, PageRequest pageRequest)
    {
        IQueryable<Payment> query = _repo.GetQueryable();

        query = query.Filter("PaymentMethod", dtoFilter?.PaymentMethod, FilterType.CONTAINS)
            .RangeValue("Amount", dtoFilter?.MinAmount, dtoFilter?.MaxAmount)
            .RangeDate("PaymentDate", dtoFilter?.StartDate, dtoFilter?.EndDate);

        return await _repo.FindAllPageAsync(
            query,
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
    

    public async Task<Payment?> GetPaymentById(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public String CreatePaymentURL(VnpayPaymentRequest request)
    {
        try
        {
            request.Money *= 10000;
            return vnpayClient.CreatePaymentUrl(request).Url;
        }
        catch (VnpayException e)
        {
            throw APIException.InternalServerError(e.Message);
        }
        catch (Exception e)
        {
            throw APIException.InternalServerError(e.Message);
        }
    }

    public VnpayPaymentResult callbackPayment(HttpRequest request)
    {
        try
        {   
            return vnpayClient.GetPaymentResult(request);
        }
        catch (VnpayException e)
        {
            throw APIException.InternalServerError(e.Message);
        }
        catch (Exception e)
        {
            throw APIException.InternalServerError(e.Message);
        }
    }
}