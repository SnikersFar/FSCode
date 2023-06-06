using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class OrderModel : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Contacts { get; set; }
        public virtual UserModel Customer { get; set; }

    }
}
