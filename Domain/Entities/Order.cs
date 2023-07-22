using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Order : BaseEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 3)]
        public string Description { get; set; }
        public OrderLevel StatusLevel { get; set; }
        public string? Contacts { get; set; }

        public virtual User Customer { get; set; }
        public virtual User Executor { get; set; }

        public virtual IList<Message> Messages { get; set; }

        public Order(string title, string description, OrderLevel statusLevel, User customer)
        {
            Title = title;
            Description = description;
            StatusLevel = statusLevel;
            Customer = customer;
            Messages = new List<Message>();
        }
    }
}
