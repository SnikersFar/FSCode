using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(StuffContext context) : base(context)
        {
        }
    }
}
