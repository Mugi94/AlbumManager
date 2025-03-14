using BusinessObjects.Entity;

namespace BusinessObjects.Entity
{
    public interface IGenericRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
    }
}