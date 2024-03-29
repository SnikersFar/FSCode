﻿using Domain.Entities;
using Infrastructure.Contexts.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(StuffContext context)
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
