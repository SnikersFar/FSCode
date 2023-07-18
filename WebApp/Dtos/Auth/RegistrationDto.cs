using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos.Auth
{
    public class RegistrationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
