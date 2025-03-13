using BusinessObjects.Entity;
using BusinessObjects.Enum;

namespace DataAccessLayer.Repository
{
    public class RecordRepository
    {
        public IEnumerable<Record> GetAll()
        {
            return [
                new Record(1, "Title", new DateTime(2000, 1, 1), TypeRecord.Album, [], []),
                new Record(2, "Title", new DateTime(2020, 6, 29), TypeRecord.EP, [], []),
                new Record(3, "Title", new DateTime(2025, 3, 13), TypeRecord.Single, [], [])
            ];
        }

        public Record Get(int id)
        {
            return new Record(1, "Title", new DateTime(2024, 12, 31), TypeRecord.Album, [], []);
        }
    }
}