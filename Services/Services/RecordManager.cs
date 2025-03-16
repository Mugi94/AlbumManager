using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;

namespace Services.Services
{
    public class RecordManager: IRecordManager
    {
        private readonly IGenericRepository<Record> _recordRepository;

        public RecordManager(IGenericRepository<Record> recordRepository)
        {
            _recordRepository = recordRepository;
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