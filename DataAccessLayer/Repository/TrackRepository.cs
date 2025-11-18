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

        private List<Artist> GetArtists(Track track)
        {
            return track.Artists.Select(
                artist => _musicContext.Artists.FirstOrDefault(a => a.Id == artist.Id)
                ?? throw new InvalidOperationException($"{artist.Name} not existing")
            ).ToList();
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
            var artists = GetArtists(track);
            track.Artists = artists;

            _musicContext.Tracks.Add(track);
            _musicContext.SaveChanges();
            return track;
        }

        public Track? Update(int id, Track track)
        {
            var trackUpdate = _musicContext.Tracks.Include(t => t.Artists).FirstOrDefault(t => t.Id == id);
            if (trackUpdate == null)
                return null;
            
            trackUpdate.Title = track.Title;
            trackUpdate.Duration = track.Duration;
            trackUpdate.Artists.Clear();

            var artists = GetArtists(track);
            foreach (var artist in artists)
                trackUpdate.Artists.Add(artist);

            _musicContext.SaveChanges();
            return trackUpdate;
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