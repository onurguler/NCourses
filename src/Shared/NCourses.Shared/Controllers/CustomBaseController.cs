using Microsoft.AspNetCore.Mvc;

using NCourses.Shared.Dtos;

namespace NCourses.Shared.Controllers;

public abstract class CustomBaseController : ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(Response<T> response)
    {
        var objectResult = new ObjectResult(response) { StatusCode = (int)response.StatusCode };
        return objectResult;
    }
}