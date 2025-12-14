using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.OrderAPI.dtos;
using DotNet_StoreManagement.Features.OrderAPI.mapping;
using DotNet_StoreManagement.Features.ProductAPI.dtos;

namespace DotNet_StoreManagement.Features.OrderAPI.Mappings;

public class OrderItemMapper : Profile, IOrderItemMapper
{
    private readonly IMapper _mapper;

    public OrderItemMapper()
    {
        // Cấu hình mapping
        CreateMap<OrderItem, OrderItemDTO>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
        CreateMap<OrderItemDTO, OrderItem>();

        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>();

        // Khởi tạo IMapper để dùng cho các method interface
        var config = new MapperConfiguration(cfg => cfg.AddProfile(this));
        _mapper = config.CreateMapper();
    }

    public OrderItemDTO ToDTO(OrderItem entity)
    {
        return _mapper.Map<OrderItemDTO>(entity);
    }

    public OrderItem ToEntity(OrderItemDTO dto)
    {
        return _mapper.Map<OrderItem>(dto);
    }
}
