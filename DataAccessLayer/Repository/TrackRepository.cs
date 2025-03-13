using BusinessObjects.Entity;

namespace DataAccessLayer.Repository
{
    public class TrackRepository
    {
        public IEnumerable<Track> GetAll()
        {
            return [
                new Track(1, "Title", 23000, [], []),
                new Track(2, "Title", 66000, [], []),
                new Track(3, "Title", 1000, [], [])
            ];
        }

        public Track Get(int id)
        {
            return new Track(1, "Title", 23000, [], []);
        }
    }
}