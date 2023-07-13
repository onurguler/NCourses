using System.Net;
using System.Text.Json;

using NCourses.Services.Basket.Api.Dtos;
using NCourses.Shared.Dtos;

namespace NCourses.Services.Basket.Api.Services;

public class BasketService : IBasketService
{
    private readonly RedisService _redisService;

    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<Response<BasketDto>> GetBasketAsync(string userId)
    {
        var existingBasket = await _redisService.GetDb().StringGetAsync(userId);
        if (string.IsNullOrWhiteSpace(existingBasket))
        {
            return Response<BasketDto>.Fail("Basket not found", HttpStatusCode.NotFound);
        }

        var basketDto = JsonSerializer.Deserialize<BasketDto>(existingBasket);

        return Response<BasketDto>.Success(basketDto);
    }

    public async Task<Response<bool>> SaveOrUpdateAsync(BasketDto basketDto)
    {
        var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
        return status
            ? Response<bool>.Success(HttpStatusCode.NoContent)
            : Response<bool>.Fail("Basket could not update or save", HttpStatusCode.InternalServerError);
    }

    public async Task<Response<bool>> DeleteAsync(string userId)
    {
        var status = await _redisService.GetDb().KeyDeleteAsync(userId);
        return status
            ? Response<bool>.Success(HttpStatusCode.NoContent)
            : Response<bool>.Fail("Basket not found", HttpStatusCode.NotFound);
    }
}