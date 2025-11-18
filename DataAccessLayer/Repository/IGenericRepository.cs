using BusinessObjects.Entity;

namespace DataAccessLayer.Repository
{
    public interface IGenericRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T? Get(int id);
        T Add(T entity);
        T? Update(int id, T entity);
        T? Delete(int id);
    }
}