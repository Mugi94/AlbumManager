using BusinessObjects.Entity;
using DataAccessLayer.Contexts;

namespace DataAccessLayer.Repository
{
    public class ArtistRepository: IGenericRepository<Artist>
    {
        private readonly DiscographyContext _discographyContext;

        public ArtistRepository(DiscographyContext albumContext)
        {
            _discographyContext = albumContext;
        }

        public IEnumerable<Artist> GetAll()
        {
            return _discographyContext.Artists;
        }

        public Artist Get(int id)
        {
            return _discographyContext.Artists.First(artist => artist.Id == id);
        }

        public Artist Add(Artist artist)
        {
            _discographyContext.Artists.Add(artist);
            _discographyContext.SaveChanges();
            return artist;
        }

        public Artist Delete(int id)
        {
            var artist = _discographyContext.Artists.First(artist => artist.Id == id);
            _discographyContext.Remove(artist);
            _discographyContext.SaveChanges();
            return artist;
        }
    }
}