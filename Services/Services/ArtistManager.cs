using BusinessObjects.Entity;
using DataAccessLayer.Repository;

namespace Services.Services
{
    public class ArtistManager: IArtistManager
    {
        private readonly IGenericRepository<Artist> _artistRepository;

        public ArtistManager(IGenericRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
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

        public Artist AddArtist(Artist artist)
        {
            return _artistRepository.Add(artist);
        }

        public Artist DeleteArtist(int id)
        {
            return _artistRepository.Delete(id);
        }
    }
}