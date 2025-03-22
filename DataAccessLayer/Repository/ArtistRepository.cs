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
            return _musicContext.Artists;
        }

        public Artist Get(int id)
        {
            return _musicContext.Artists.First(artist => artist.Id == id);
        }

        public Artist Add(Artist artist)
        {
            _musicContext.Artists.Add(artist);
            _musicContext.SaveChanges();
            return artist;
        }

        public Artist Delete(int id)
        {
            var artist = _musicContext.Artists.First(artist => artist.Id == id);
            _musicContext.Remove(artist);
            _musicContext.SaveChanges();
            return artist;
        }
    }
}