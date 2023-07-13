using System.Collections.Generic;
using System.Threading.Tasks;

using IdentityModel;

using IdentityServer4.Validation;

using Microsoft.AspNetCore.Identity;

using NCourses.IdentityServer.Models;

namespace NCourses.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByNameAsync(context.UserName);

            var isPasswordValid = user != null && await _userManager.CheckPasswordAsync(user, context.Password);
            if (!isPasswordValid)
            {
                var errors = new Dictionary<string, object>
                {
                    { "errors", new List<string> { "Invalid username or password." } }
                };

                context.Result.CustomResponse = errors;
                return;
            }

            context.Result = new GrantValidationResult(user.Id, OidcConstants.AuthenticationMethods.Password);
        }
    }
}