using BusinessObjects.Entity;

namespace DataAccessLayer.Repository
{
    public interface IGenericRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T?> UpdateAsync(int id, T entity);
        Task<T?> DeleteAsync(int id);
    }
}