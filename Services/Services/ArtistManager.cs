using BusinessObjects.Entity;
using DataAccessLayer.Repository;

namespace Services.Services
{
    public class ArtistManager : IArtistManager
    {
        private readonly IGenericRepository<Artist> _artistRepository;

        public ArtistManager(IGenericRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<IEnumerable<Artist>> GetArtistsAsync()
        {
            return await _artistRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Artist>> GetArtistsAsync(int year)
        {
            var artists = await _artistRepository.GetAllAsync();
            return artists.Where(artist => artist.DebutYear == year);
        }

        public async Task<Artist?> FindArtistAsync(int id)
        {
            return await _artistRepository.GetAsync(id);
        }

        public async Task<Artist?> AddArtistAsync(Artist artist)
        {
            var existing = await _artistRepository.GetAllAsync();
            if (existing.FirstOrDefault(a => a.Id == artist.Id) != null)
                return null;

            return await _artistRepository.AddAsync(artist);
        }

        public async Task<Artist?> UpdateArtistAsync(int id, Artist artist)
        {
            return await _artistRepository.UpdateAsync(id, artist);
        }

        public async Task<Artist?> DeleteArtistAsync(int id)
        {
            return await _artistRepository.DeleteAsync(id);
        }
    }
}