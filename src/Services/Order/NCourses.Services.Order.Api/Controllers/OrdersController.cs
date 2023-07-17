using MediatR;

using Microsoft.AspNetCore.Mvc;

using NCourses.Services.Order.Application.Commands;
using NCourses.Services.Order.Application.Queries;
using NCourses.Shared.Controllers;
using NCourses.Shared.Services;

namespace NCourses.Services.Order.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : CustomBaseController
{
    private readonly IMediator _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;

    public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
    {
        _mediator = mediator;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var response = await _mediator.Send(new GetOrdersByUserQuery(_sharedIdentityService.CurrentUserId));
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrder([FromBody] CreateOrderCommand command)
    {
        var response = await _mediator.Send(command);
        return CreateActionResultInstance(response);
    }
}