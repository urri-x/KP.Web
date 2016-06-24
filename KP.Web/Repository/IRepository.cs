using System.Linq;

namespace KP.WebApi.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        int Update(T entity);
        T Add(T entity);
        void Delete(int id);
    }
}