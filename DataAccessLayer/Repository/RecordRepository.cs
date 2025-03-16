using BusinessObjects.Entity;
using DataAccessLayer.Contexts;

namespace DataAccessLayer.Repository
{
    public class RecordRepository: IGenericRepository<Record>
    {
        private readonly DiscographyContext _discographyContext;
        
        public RecordRepository(DiscographyContext albumContext)
        {
            _discographyContext = albumContext;
        }

        public IEnumerable<Record> GetAll()
        {
            return _discographyContext.Records;
        }

        public Record Get(int id)
        {
            return _discographyContext.Records.First(record => record.Id == id);
        }

        public Record Add(Record record)
        {
            _discographyContext.Records.Add(record);
            _discographyContext.SaveChanges();
            return record;
        }

        public Record Delete(int id)
        {
            var record = _discographyContext.Records.First(record => record.Id == id);
            _discographyContext.Remove(record);
            _discographyContext.SaveChanges();
            return record;
        }
    }
}