using IdentityModel.Client;

using NCourses.Shared.Dtos;
using NCourses.Web.Models;

namespace NCourses.Web.Services.Interfaces;

public interface IIdentityService
{
    Task<Response<bool>> SignInAsync(SignInInput signInInput);
    Task<TokenResponse> GetAccessTokenByRefreshTokenAsync();
    Task RevokeRefreshTokenAsync();
}