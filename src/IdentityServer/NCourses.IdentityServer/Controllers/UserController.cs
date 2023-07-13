using System.Linq;
using System.Threading.Tasks;

using IdentityServer4;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using NCourses.IdentityServer.Dtos;
using NCourses.IdentityServer.Models;
using NCourses.Shared.Dtos;

namespace NCourses.IdentityServer.Controllers
{
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            var user = new ApplicationUser
            {
                UserName = signUpDto.UserName, Email = signUpDto.Email, City = signUpDto.City,
            };

            var result = await _userManager.CreateAsync(user, signUpDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContentResponse>.Fail(result.Errors.Select(x => x.Description).ToList()));
            }

            return NoContent();
        }
    }
}