using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuthUser : User
    {
        public AuthUser(string name, string login, string hashPassword, UserRole role)
        : base(name, login, hashPassword, role)
        {
        }

        public string? Token { get; set; }
        public DateTime? TokenTime { get; set; }
    }
}
