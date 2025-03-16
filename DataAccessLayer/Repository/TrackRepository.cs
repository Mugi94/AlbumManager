using BusinessObjects.Entity;
using DataAccessLayer.Contexts;

namespace DataAccessLayer.Repository
{
    public class TrackRepository: IGenericRepository<Track>
    {
        private readonly DiscographyContext _discographyContext;

        public TrackRepository(DiscographyContext albumContext)
        {
            _discographyContext = albumContext;
        }
        public IEnumerable<Track> GetAll()
        {
            return _discographyContext.Tracks;
        }

        public Track Get(int id)
        {
            return _discographyContext.Tracks.First(track => track.Id == id);
        }

        public Track Add(Track track)
        {
            _discographyContext.Tracks.Add(track);
            _discographyContext.SaveChanges();
            return track;
        }

        public Track Delete(int id)
        {
            var track = _discographyContext.Tracks.First(track => track.Id == id);
            _discographyContext.Remove(track);
            _discographyContext.SaveChanges();
            return track;
        }
    }
}