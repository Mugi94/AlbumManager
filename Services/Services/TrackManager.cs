using BusinessObjects.Entity;
using DataAccessLayer.Repository;

namespace Services.Services
{
    public class TrackManager : ITrackManager
    {
        private readonly IGenericRepository<Track> _trackRepository;

        public TrackManager(IGenericRepository<Track> trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task<IEnumerable<Track>> GetTracksAsync()
        {
            return await _trackRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Track>> GetTracksAsync(Artist artist)
        {
            var tracks = await _trackRepository.GetAllAsync();
            return tracks.Where(track => track.Artists.Any(a => a.Id == artist.Id));
        }

        public async Task<Track?> FindTrackAsync(int id)
        {
            return await _trackRepository.GetAsync(id);
        }

        public async Task<Track?> AddTrackAsync(Track track)
        {
            var existing = await _trackRepository.GetAllAsync();
            if (existing.FirstOrDefault(t => t.Id == track.Id) != null)
                return null;

            return await _trackRepository.AddAsync(track);
        }

        public async Task<Track?> UpdateTrackAsync(int id, Track track)
        {
            return await _trackRepository.UpdateAsync(id, track);
        }

        public async Task<Track?> DeleteTrackAsync(int id)
        {
            return await _trackRepository.DeleteAsync(id);
        }
    }
}