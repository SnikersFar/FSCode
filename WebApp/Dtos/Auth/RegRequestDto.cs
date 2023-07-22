using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos.Auth
{
    public class RegRequestDto
    {
        [Required]
        public string? NickName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
