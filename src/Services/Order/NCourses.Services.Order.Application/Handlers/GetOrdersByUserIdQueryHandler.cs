using MediatR;

using Microsoft.EntityFrameworkCore;

using NCourses.Services.Order.Application.Dtos;
using NCourses.Services.Order.Application.Mapping;
using NCourses.Services.Order.Application.Queries;
using NCourses.Services.Order.Infrastructure;
using NCourses.Shared.Dtos;

namespace NCourses.Services.Order.Application.Handlers;

public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserQuery, Response<List<OrderDto>>>
{
    private readonly OrderDbContext _context;

    public GetOrdersByUserIdQueryHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Include(x => x.OrderItems)
            .Where(x => x.BuyerId == request.UserId)
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync(cancellationToken);

        if (!orders.Any())
            return Response<List<OrderDto>>.Success(new List<OrderDto>());

        var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
        
        return Response<List<OrderDto>>.Success(ordersDto);
    }
}