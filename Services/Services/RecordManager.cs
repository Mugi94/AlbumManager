using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;

namespace Services.Services
{
    public class RecordManager
    {
        private readonly RecordRepository _recordRepository;

        public RecordManager()
        {
            _recordRepository = new RecordRepository();
        }

        public IEnumerable<Record> GetRecords()
        {
            return _recordRepository.GetAll();
        }

        public IEnumerable<Record> GetRecords(TypeRecord type)
        {
            return _recordRepository.GetAll().Where(record => record.Type == type);
        }

        public Record FindRecord(int id)
        {
            return _recordRepository.Get(id);
        }
    }
}