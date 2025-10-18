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
            var artists = record.Artists
            .Select(
                artist => _musicContext.Artists.FirstOrDefault(a => a.Id == artist.Id)
                ?? throw new InvalidOperationException($"{artist.Name} not existing")
            ).ToList();

            var tracks = record.Tracks.Select(
                track => _musicContext.Tracks.FirstOrDefault(t => t.Id == track.Id)
                ?? throw new InvalidOperationException($"{track.Title} not existing")
            ).ToList();

            record.Artists = artists;
            record.Tracks = tracks;

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