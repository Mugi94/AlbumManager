using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;

namespace Services.Services
{
    public class RecordManager : IRecordManager
    {
        private readonly IGenericRepository<Record> _recordRepository;

        public RecordManager(IGenericRepository<Record> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<IEnumerable<Record>> GetRecordsAsync()
        {
            return await _recordRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Record>> GetRecordsAsync(TypeRecord type)
        {
            var records = await _recordRepository.GetAllAsync();
            return records.Where(record => record.Type == type);
        }

        public async Task<Record?> FindRecordAsync(int id)
        {
            return await _recordRepository.GetAsync(id);
        }

        public async Task<Record?> AddRecordAsync(Record record)
        {
            var existing = await _recordRepository.GetAllAsync();
            if (existing.FirstOrDefault(r => r.Id == record.Id) != null)
                return null;

            return await _recordRepository.AddAsync(record);
        }

        public async Task<Record?> UpdateRecordAsync(int id, Record record)
        {
            return await _recordRepository.UpdateAsync(id, record);
        }

        public async Task<Record?> DeleteRecordAsync(int id)
        {
            return await _recordRepository.DeleteAsync(id);
        }
    }
}