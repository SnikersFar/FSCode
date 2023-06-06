using Data.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    where T : BaseModel
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> SaveAsync(T model)
        {
            if (model.Id > 0)
            {
                _dbSet.Update(model);
            }
            else
            {
                _dbSet.Add(model);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public bool Delete(T model)
        {
            _dbSet.Remove(model);
            return true;
        }

        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int Id)
        {
            return _dbSet.SingleOrDefault(e => e.Id == Id);
        }
    }
}
