using BusinessObjects.Entity;
using DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class RecordRepository: IGenericRepository<Record>
    {
        private readonly MusicContext _musicContext;
        
        public RecordRepository(MusicContext albumContext)
        {
            _musicContext = albumContext;
        }

        private List<Artist> GetArtists(Record record)
        {
            return record.Artists.Select(
                artist => _musicContext.Artists.FirstOrDefault(a => a.Id == artist.Id)
                ?? throw new InvalidOperationException($"{artist.Name} not existing")
            ).ToList();
        }

        private List<Track> GetTracks(Record record)
        {
            return record.Tracks.Select(
                track => _musicContext.Tracks.FirstOrDefault(t => t.Id == track.Id)
                ?? throw new InvalidOperationException($"{track.Title} not existing")
            ).ToList();
        }

        public IEnumerable<Record> GetAll()
        {
            return _musicContext.Records
                .Include(r => r.Artists)
                .Include(r => r.Tracks)
                .ToList();
        }

        public Record? Get(int id)
        {
            var record = _musicContext.Records
                .Include(r => r.Artists)
                .Include(r => r.Tracks)
                .FirstOrDefault(record => record.Id == id);

            if (record == null)
                return null;

            return record;
        }

        public Record Add(Record record)
        {
            var artists = GetArtists(record);
            var tracks = GetTracks(record);

            record.Artists = artists;
            record.Tracks = tracks;

            _musicContext.Records.Add(record);
            _musicContext.SaveChanges();
            return record;
        }

        public Record? Update(int id, Record record)
        {
            var recordUpdate = _musicContext.Records.Find(id);
            if (recordUpdate == null)
                return null;
            
            recordUpdate.Title = record.Title;
            recordUpdate.ReleaseDate = record.ReleaseDate;
            recordUpdate.Type = record.Type;
            recordUpdate.Artists.Clear();
            recordUpdate.Tracks.Clear();
            
            var artists = GetArtists(record);
            foreach (var artist in artists)
                recordUpdate.Artists.Add(artist);

            var tracks = GetTracks(record);
            foreach (var track in tracks)
                recordUpdate.Tracks.Add(track);

            _musicContext.SaveChanges();
            return recordUpdate;
        }

        public Record? Delete(int id)
        {
            var record = _musicContext.Records.FirstOrDefault(record => record.Id == id);
            if (record == null)
                return null;

            _musicContext.Remove(record);
            _musicContext.SaveChanges();
            return record;
        }
    }
}