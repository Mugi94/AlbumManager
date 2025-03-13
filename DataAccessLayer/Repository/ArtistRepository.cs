using BusinessObjects.Entity;

namespace DataAccessLayer.Repository
{
    public class ArtistRepository
    {
        public IEnumerable<Artist> GetAll()
        {
            return [
                new Artist(1, "Artist1", 2000),
                new Artist(2, "Artist2", 2020),
                new Artist(3, "Artist3", 2025)
            ];
        }

        public Artist Get(int id)
        {
            return new Artist(1, "Artist", 2025);
        }
    }
}