using BusinessObjects.Enum;

namespace BusinessObjects.DataTransferObject
{
    public class RecordRequest
    {
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public TypeRecord Type { get; set; }

        public ICollection<ArtistSummary> Artists { get; set; } = new List<ArtistSummary>();
        public ICollection<TrackSummary> Tracks { get; set; } = new List<TrackSummary>();
    }
}