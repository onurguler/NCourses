using Microsoft.AspNetCore.Mvc;

using NCourses.Shared.Controllers;
using NCourses.Shared.Dtos;

namespace NCourses.Services.Payment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : CustomBaseController
{
    [HttpPost]
    public IActionResult ReceivePayment()
    {
        return CreateActionResultInstance(Response<bool>.Success(true));
    }
}