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
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Contacts { get; set; }
        public virtual User Customer { get; set; }

    }
}
