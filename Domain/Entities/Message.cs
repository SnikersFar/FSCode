using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Message : BaseEntity
    {
        [Required]
        public string Value { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

        public virtual User? Creator { get; set; }
        public virtual Order Order { get; set; } = null!;

        public Message(string message, DateTime dateCreated)
        {
            DateCreated = dateCreated;
            Value = message;
        }
        public Message(string message) : this(message, DateTime.UtcNow)
        {
        }
    }
}
