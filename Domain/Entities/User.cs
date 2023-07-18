using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Login { get; set; }
        [Required]
        public UserRole Role { get; set; }
        public string Email { get; set; }

        [Required]
        public string HashPassword { get; set; }

        public string? Token { get; set; }
        public DateTime? TokenTime { get; set; }


        public virtual IList<Order> Orders { get; set; }
    }
}
