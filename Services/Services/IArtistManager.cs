using BusinessObjects.Entity;

namespace Services.Services
{
    public interface IArtistManager
    {
        public IEnumerable<Artist> GetArtists();
        public IEnumerable<Artist> GetArtists(int year);
        public Artist FindArtist(int id);
    }
}