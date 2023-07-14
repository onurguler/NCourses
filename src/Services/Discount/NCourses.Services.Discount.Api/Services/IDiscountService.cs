using NCourses.Shared.Dtos;

namespace NCourses.Services.Discount.Api.Services;

public interface IDiscountService
{
    Task<Response<List<Models.Discount>>> GetAllAsync();
    Task<Response<List<Models.Discount>>> GetAllByUserIdAsync(string userId);
    Task<Response<Models.Discount>> GetByIdAsync(int id);
    Task<Response<Models.Discount>> GetByCodeAndUserIdAsync(string code, string userId);
    Task<Response<NoContentResponse>> SaveAsync(Models.Discount discount);
    Task<Response<NoContentResponse>> UpdateAsync(Models.Discount discount);
    Task<Response<NoContentResponse>> DeleteAsync(int id);
}