using MediatR;

using NCourses.Services.Order.Application.Dtos;
using NCourses.Shared.Dtos;

namespace NCourses.Services.Order.Application.Commands;

public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
{
    public string BuyerId { get; set; } = null!;
    public List<OrderItemDto> OrderItems { get; set; } = null!;
    public AddressDto Address { get; set; } = null!;
}