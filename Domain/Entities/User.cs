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
        public string HashPassword { get; set; }
        [Required]
        public UserRole Role { get; set; }

        public string? Email { get; set; }
        
        public virtual IList<Order> Orders { get; set; }
        public virtual IList<Message> Messages { get; set; }

        public User(string name, string login, string hashPassword, UserRole role)
        {
            Name = name;
            HashPassword = hashPassword;
            Login = login;
            Orders = new List<Order>();
            Messages = new List<Message>();
            Role = role;
        }
    }
}
