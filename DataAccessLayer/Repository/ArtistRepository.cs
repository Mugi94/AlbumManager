using BusinessObjects.Entity;
using DataAccessLayer.Contexts;

namespace DataAccessLayer.Repository
{
    public class ArtistRepository: IGenericRepository<Artist>
    {
        private readonly MusicContext _musicContext;

        public ArtistRepository(MusicContext albumContext)
        {
            _musicContext = albumContext;
        }

        public IEnumerable<Artist> GetAll()
        {
            return _musicContext.Artists.ToList();
        }

        public Artist? Get(int id)
        {
            var artist = _musicContext.Artists.FirstOrDefault(artist => artist.Id == id);
            if (artist == null)
                return null;

            return artist;
        }

        public Artist Add(Artist artist)
        {
            _musicContext.Artists.Add(artist);
            _musicContext.SaveChanges();
            return artist;
        }

        public Artist? Delete(int id)
        {
            var artist = _musicContext.Artists.FirstOrDefault(artist => artist.Id == id);
            if (artist == null)
                return null;
            
            _musicContext.Artists.Remove(artist);
            _musicContext.SaveChanges();
            return artist;
        }
    }
}