using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class OrderRepository : BaseRepository<OrderModel>
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }
    }
}
