using Domain.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        public T GetAll();
        public T GetById(int Id);
        public bool Add(T model);
        public bool Change(T model);
        public bool Delete(T model);
    }
}
