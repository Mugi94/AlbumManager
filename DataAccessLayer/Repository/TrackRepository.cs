using BusinessObjects.Entity;
using DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class TrackRepository: IGenericRepository<Track>
    {
        private readonly MusicContext _musicContext;

        public TrackRepository(MusicContext albumContext)
        {
            _musicContext = albumContext;
        }
        public IEnumerable<Track> GetAll()
        {
            return _musicContext.Tracks
                .Include(t => t.Artists)
                .ToList();
        }

        public Track? Get(int id)
        {
            var track = _musicContext.Tracks
                .Include(t => t.Artists)
                .FirstOrDefault(track => track.Id == id);
                    
            if (track == null)
                return null;

            return track;
        }

        public Track Add(Track track)
        {
            var artists = track.Artists.Select(
                artist => _musicContext.Artists.FirstOrDefault(a => a.Id == artist.Id)
                ?? throw new InvalidOperationException($"{artist.Name} not existing")
            ).ToList();

            track.Artists = artists;

            _musicContext.Tracks.Add(track);
            _musicContext.SaveChanges();
            return track;
        }

        public Track? Delete(int id)
        {
            var track = _musicContext.Tracks.FirstOrDefault(track => track.Id == id);
            if (track == null)
                return null;

            _musicContext.Remove(track);
            _musicContext.SaveChanges();
            return track;
        }
    }
}