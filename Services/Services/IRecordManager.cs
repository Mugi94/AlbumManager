using BusinessObjects.Entity;
using BusinessObjects.Enum;

namespace Services.Services
{
    public interface IRecordManager
    {
        public IEnumerable<Record> GetRecords();
        public IEnumerable<Record> GetRecords(TypeRecord type);
        public Record? FindRecord(int id);
        public Record? AddRecord(Record record);
        public Record? DeleteRecord(int id);
    }
}