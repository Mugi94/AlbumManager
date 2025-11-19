using BusinessObjects.Entity;

namespace Services.Services
{
    public interface IArtistManager
    {
        Task<IEnumerable<Artist>> GetArtistsAsync();
        Task<IEnumerable<Artist>> GetArtistsAsync(int year);
        Task<Artist?> FindArtistAsync(int id);
        Task<Artist?> AddArtistAsync(Artist artist);
        Task<Artist?> UpdateArtistAsync(int id, Artist artist);
        Task<Artist?> DeleteArtistAsync(int id);
    }
}