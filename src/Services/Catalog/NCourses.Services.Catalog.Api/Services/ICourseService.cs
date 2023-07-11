using NCourses.Services.Catalog.Api.Dtos;
using NCourses.Shared.Dtos;

namespace NCourses.Services.Catalog.Api.Services;

public interface ICourseService
{
    Task<Response<List<CourseDto>>> GetAllAsync();
    Task<Response<CourseDto>> GetByIdAsync(string courseId);
    Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);
    Task<Response<CourseDto>> CreateAsync(CourseCreateDto input);
    Task<Response<NoContentResponse>> UpdateAsync(CourseUpdateDto input);
    Task<Response<NoContentResponse>> DeleteAsync(string courseId);
}