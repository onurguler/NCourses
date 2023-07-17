using AutoMapper;

using NCourses.Services.Order.Application.Dtos;

namespace NCourses.Services.Order.Application.Mapping;

public class CustomMapping : Profile
{
    public CustomMapping()
    {
        CreateMap<Domain.OrderAggregate.Order, OrderDto>();
        CreateMap<Domain.OrderAggregate.OrderItem, OrderItemDto>();
        CreateMap<Domain.OrderAggregate.Address, AddressDto>();
    }
}