using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos.Auth
{
    public class AuthRequestDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
