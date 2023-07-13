using Microsoft.AspNetCore.Mvc;

using NCourses.Services.Basket.Api.Dtos;
using NCourses.Services.Basket.Api.Services;
using NCourses.Shared.Controllers;
using NCourses.Shared.Services;

namespace NCourses.Services.Basket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketsController : CustomBaseController
{
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
    {
        _basketService = basketService;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyBasket()
    {
        var basket = await _basketService.GetBasketAsync(_sharedIdentityService.CurrentUserId);
        return CreateActionResultInstance(basket);
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
    {
        basketDto.UserId = _sharedIdentityService.CurrentUserId;
        var response = await _basketService.SaveOrUpdateAsync(basketDto);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasket()
    {
        var response = await _basketService.DeleteAsync(_sharedIdentityService.CurrentUserId);
        return CreateActionResultInstance(response);
    }
}