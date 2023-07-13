using Microsoft.AspNetCore.Http;

namespace NCourses.Shared.Services;

public class SharedIdentityService : ISharedIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string CurrentUserId => _httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == "sub").Value;
}