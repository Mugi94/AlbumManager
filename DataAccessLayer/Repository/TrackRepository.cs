using BusinessObjects.Entity;
using DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class TrackRepository : IGenericRepository<Track>
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

        public async Task<IEnumerable<Track>> GetAllAsync()
            => await _musicContext.Tracks
                .Include(t => t.Artists)
                .ToListAsync();

        public async Task<Track?> GetAsync(int id)
        {
            return await _musicContext.Tracks
                .Include(t => t.Artists)
                .FirstOrDefaultAsync(track => track.Id == id);
        }

        public async Task<Track> AddAsync(Track track)
        {
            var artists = GetArtists(track);
            track.Artists = artists;

            _musicContext.Tracks.Add(track);
            await _musicContext.SaveChangesAsync();
            return track;
        }

        public async Task<Track?> UpdateAsync(int id, Track track)
        {
            var trackUpdate = await _musicContext.Tracks.Include(t => t.Artists).FirstOrDefaultAsync(t => t.Id == id);
            if (trackUpdate == null)
                return null;

            trackUpdate.Title = track.Title;
            trackUpdate.Duration = track.Duration;
            trackUpdate.Artists.Clear();

            var artists = GetArtists(track);
            foreach (var artist in artists)
                trackUpdate.Artists.Add(artist);

            await _musicContext.SaveChangesAsync();
            return trackUpdate;
        }

        public async Task<Track?> DeleteAsync(int id)
        {
            var track = await _musicContext.Tracks.FindAsync(id);
            if (track == null)
                return null;

            _musicContext.Remove(track);
            await _musicContext.SaveChangesAsync();
            return track;
        }
    }
}