using BusinessObjects.Entity;
using DataAccessLayer.Repository;

namespace Services.Services
{
    public class TrackManager
    {
        private readonly TrackRepository _trackRepository;

        public TrackManager()
        {
            _trackRepository = new TrackRepository();
        }

        public IEnumerable<Track> GetTracks()
        {
            return _trackRepository.GetAll();
        }

        public IEnumerable<Track> GetTracks(Artist artist)
        {
            return _trackRepository.GetAll().Where(track => track.Artists.Any(a => a.Id == artist.Id));
        }

        public Track FindTrack(int id)
        {
            return _trackRepository.Get(id);
        }
    }
}