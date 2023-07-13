using Microsoft.AspNetCore.Mvc;

using NCourses.Services.PhotoStock.Api.Dtos;
using NCourses.Shared.Controllers;
using NCourses.Shared.Dtos;

namespace NCourses.Services.PhotoStock.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotosController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> SavePhoto([FromForm] IFormFile? photo, CancellationToken cancellationToken)
    {
        if (photo is not { Length: > 0 })
        {
            return BadRequest(Response<PhotoDto>.Fail("Photo is empty or null."));
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

        await using var stream = new FileStream(path, FileMode.Create);
        await photo.CopyToAsync(stream, cancellationToken);

        var returnPath = "photos/" + photo.FileName;
        
        PhotoDto photoDto = new() { Url = returnPath };
        
        return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto));
    }
}