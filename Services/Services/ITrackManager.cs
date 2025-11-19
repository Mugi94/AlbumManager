using BusinessObjects.Entity;

namespace Services.Services
{
    public interface ITrackManager
    {
        Task<IEnumerable<Track>> GetTracksAsync();
        Task<IEnumerable<Track>> GetTracksAsync(Artist artist);
        Task<Track?> FindTrackAsync(int id);
        Task<Track?> AddTrackAsync(Track track);
        Task<Track?> UpdateTrackAsync(int id, Track track);
        Task<Track?> DeleteTrackAsync(int id);
    }
}