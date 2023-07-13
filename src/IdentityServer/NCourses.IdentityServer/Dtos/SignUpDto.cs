using System.ComponentModel.DataAnnotations;

namespace NCourses.IdentityServer.Dtos
{
    public class SignUpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string City { get; set; }
    }
}