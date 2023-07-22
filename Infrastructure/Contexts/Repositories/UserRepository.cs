using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(StuffContext context) : base(context)
        {
        }
    }
}
