using BusinessObjects.Entity;

namespace Services.Services
{
    public interface ITrackManager
    {
        public IEnumerable<Track> GetTracks();
        public IEnumerable<Track> GetTracks(Artist artist);
        public Track? FindTrack(int id);
        public Track? AddTrack(Track track);
        public Track? DeleteTrack(int id);
    }
}