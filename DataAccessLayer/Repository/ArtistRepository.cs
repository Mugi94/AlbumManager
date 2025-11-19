using BusinessObjects.Entity;
using DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class ArtistRepository : IGenericRepository<Artist>
    {
        private readonly MusicContext _musicContext;

        public ArtistRepository(MusicContext albumContext)
        {
            _musicContext = albumContext;
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _musicContext.Artists.ToListAsync();
        }

        public async Task<Artist?> GetAsync(int id)
        {
            return await _musicContext.Artists.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Artist> AddAsync(Artist artist)
        {
            _musicContext.Artists.Add(artist);
            await _musicContext.SaveChangesAsync();
            return artist;
        }

        public async Task<Artist?> UpdateAsync(int id, Artist artist)
        {
            var artistUpdate = await _musicContext.Artists.FindAsync(id);
            if (artistUpdate == null)
                return null;

            artistUpdate.Name = artist.Name;
            artistUpdate.DebutYear = artist.DebutYear;

            await _musicContext.SaveChangesAsync();
            return artistUpdate;
        }

        public async Task<Artist?> DeleteAsync(int id)
        {
            var artist = await _musicContext.Artists.FindAsync(id);
            if (artist == null)
                return null;

            _musicContext.Artists.Remove(artist);
            await _musicContext.SaveChangesAsync();
            return artist;
        }
    }
}