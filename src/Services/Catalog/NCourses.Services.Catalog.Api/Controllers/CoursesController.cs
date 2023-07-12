using Microsoft.AspNetCore.Mvc;

using NCourses.Services.Catalog.Api.Dtos;
using NCourses.Services.Catalog.Api.Services;
using NCourses.Shared.Controllers;

namespace NCourses.Services.Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : CustomBaseController
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _courseService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _courseService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpGet("GetAllByUserId/{userId}")]
    public async Task<IActionResult> GetAllByUserId(string userId)
    {
        var response = await _courseService.GetAllByUserIdAsync(userId);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CourseCreateDto input)
    {
        var response = await _courseService.CreateAsync(input);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CourseUpdateDto input)
    {
        var response = await _courseService.UpdateAsync(input);
        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _courseService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }
}