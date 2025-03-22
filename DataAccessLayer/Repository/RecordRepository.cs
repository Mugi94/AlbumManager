using BusinessObjects.Entity;
using DataAccessLayer.Contexts;

namespace DataAccessLayer.Repository
{
    public class RecordRepository: IGenericRepository<Record>
    {
        private readonly MusicContext _musicContext;
        
        public RecordRepository(MusicContext albumContext)
        {
            _musicContext = albumContext;
        }

        public IEnumerable<Record> GetAll()
        {
            return _musicContext.Records;
        }

        public Record Get(int id)
        {
            return _musicContext.Records.First(record => record.Id == id);
        }

        public Record Add(Record record)
        {
            _musicContext.Records.Add(record);
            _musicContext.SaveChanges();
            return record;
        }

        public Record Delete(int id)
        {
            var record = _musicContext.Records.First(record => record.Id == id);
            _musicContext.Remove(record);
            _musicContext.SaveChanges();
            return record;
        }
    }
}