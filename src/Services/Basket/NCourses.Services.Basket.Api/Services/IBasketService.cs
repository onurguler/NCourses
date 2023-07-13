using NCourses.Services.Basket.Api.Dtos;
using NCourses.Shared.Dtos;

namespace NCourses.Services.Basket.Api.Services;

public interface IBasketService
{
    Task<Response<BasketDto>> GetBasketAsync(string userId);
    Task<Response<bool>> SaveOrUpdateAsync(BasketDto basketDto);
    Task<Response<bool>> DeleteAsync(string userId);
}