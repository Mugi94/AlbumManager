using BusinessObjects.Entity;
using BusinessObjects.Enum;

namespace Services.Services
{
    public interface IRecordManager
    {
        Task<IEnumerable<Record>> GetRecordsAsync();
        Task<IEnumerable<Record>> GetRecordsAsync(TypeRecord type);
        Task<Record?> FindRecordAsync(int id);
        Task<Record?> AddRecordAsync(Record record);
        Task<Record?> UpdateRecordAsync(int id, Record record);
        Task<Record?> DeleteRecordAsync(int id);
    }
}