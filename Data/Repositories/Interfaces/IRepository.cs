using Domain.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        public IList<T> GetAll();
        public T GetById(int Id);
        public Task<bool> SaveAsync(T model);
        public bool Delete(T model);
    }
}
