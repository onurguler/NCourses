using System.Net;

using MediatR;

using NCourses.Services.Order.Application.Commands;
using NCourses.Services.Order.Application.Dtos;
using NCourses.Services.Order.Domain.OrderAggregate;
using NCourses.Services.Order.Infrastructure;
using NCourses.Shared.Dtos;

namespace NCourses.Services.Order.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
{
    private readonly OrderDbContext _context;

    public CreateOrderCommandHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var address = new Address(
            request.Address.Province,
            request.Address.District,
            request.Address.Street,
            request.Address.ZipCode,
            request.Address.Line);

        var order = new Domain.OrderAggregate.Order(request.BuyerId, address);

        request.OrderItems.ForEach(orderItem =>
        {
            order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.PictureUrl, orderItem.Price);
        });

        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var createdOrderDto = new CreatedOrderDto(order.Id);
        return Response<CreatedOrderDto>.Success(createdOrderDto, HttpStatusCode.Created);
    }
}