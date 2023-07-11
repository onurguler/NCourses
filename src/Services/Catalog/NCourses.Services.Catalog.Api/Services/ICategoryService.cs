using NCourses.Services.Catalog.Api.Dtos;
using NCourses.Shared.Dtos;

namespace NCourses.Services.Catalog.Api.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> GetByIdAsync(string categoryId);
    Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto input);
}