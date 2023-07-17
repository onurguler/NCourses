using MediatR;

using NCourses.Services.Order.Application.Dtos;
using NCourses.Shared.Dtos;

namespace NCourses.Services.Order.Application.Queries;

public record GetOrdersByUserQuery(string UserId) : IRequest<Response<List<OrderDto>>>;
