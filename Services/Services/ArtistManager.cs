using BusinessObjects.Entity;
using DataAccessLayer.Repository;

namespace Services.Services
{
    public class ArtistManager
    {
        private readonly ArtistRepository _artistRepository;

        public ArtistManager()
        {
            _artistRepository = new ArtistRepository();
        }

        public IEnumerable<Artist> GetArtists()
        {
            return _artistRepository.GetAll();
        }

        public IEnumerable<Artist> GetArtists(int year)
        {
            return _artistRepository.GetAll().Where(artist => artist.DebutYear == year);
        }

        public Artist FindArtist(int id)
        {
            return _artistRepository.Get(id);
        }
    }
}