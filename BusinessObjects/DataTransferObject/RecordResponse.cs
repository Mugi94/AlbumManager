using BusinessObjects.Enum;

namespace BusinessObjects.DataTransferObject
{
    public class RecordDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public TypeRecord Type { get; set; }

        public ICollection<ArtistDto> Artists { get; set; } = new List<ArtistDto>();
        public ICollection<TrackDto> Tracks { get; set; } = new List<TrackDto>();
    }
}