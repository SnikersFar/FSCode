using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserModel : BaseModel
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
        public Roles Role { get; set; }
        public string Email { get; set; }


        public virtual IList<OrderModel> Orders { get; set; }
    }
}
