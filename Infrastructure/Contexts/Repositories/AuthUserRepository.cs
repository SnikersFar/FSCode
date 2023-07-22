using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts.Repositories
{
    public class AuthUserRepository : BaseRepository<AuthUser>
    {
        public AuthUserRepository(StuffContext context) : base(context)
        {
        }
        public AuthUser? GetUserBy(Func<AuthUser, bool> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }
    }
}
