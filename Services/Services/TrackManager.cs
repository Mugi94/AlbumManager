using BusinessObjects.Entity;
using DataAccessLayer.Repository;

namespace Services.Services
{
    public class TrackManager: ITrackManager
    {
        private readonly IGenericRepository<Track> _trackRepository;

        public TrackManager(IGenericRepository<Track> trackRepository)
        {
            _trackRepository = trackRepository;
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

        public Track AddTrack(Track track)
        {
            return _trackRepository.Add(track);
        }

        public Track DeleteTrack(int id)
        {
            return _trackRepository.Delete(id);
        }
    }
}