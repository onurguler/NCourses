using Microsoft.AspNetCore.Mvc;

using NCourses.Services.Discount.Api.Services;
using NCourses.Shared.Controllers;
using NCourses.Shared.Services;

namespace NCourses.Services.Discount.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountsController : CustomBaseController
{
    private readonly IDiscountService _discountService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
    {
        _discountService = discountService;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _discountService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetByUserId()
    {
        var response = await _discountService.GetAllByUserIdAsync(_sharedIdentityService.CurrentUserId);
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _discountService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpGet("[action]/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var response = await _discountService.GetByCodeAndUserIdAsync(code, _sharedIdentityService.CurrentUserId);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Save(Models.Discount discount)
    {
        var response = await _discountService.SaveAsync(discount);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Models.Discount discount)
    {
        var response = await _discountService.UpdateAsync(discount);
        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _discountService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }
}