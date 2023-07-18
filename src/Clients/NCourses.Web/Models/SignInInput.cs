using System.ComponentModel.DataAnnotations;

namespace NCourses.Web.Models;

public class SignInInput
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}