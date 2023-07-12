using Microsoft.AspNetCore.Mvc;

using NCourses.Services.Catalog.Api.Dtos;
using NCourses.Services.Catalog.Api.Services;
using NCourses.Shared.Controllers;

namespace NCourses.Services.Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : CustomBaseController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _categoryService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _categoryService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDto input)
    {
        var response = await _categoryService.CreateAsync(input);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CategoryUpdateDto input)
    {
        var response = await _categoryService.UpdateAsync(input);
        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _categoryService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }
}