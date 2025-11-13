using BusinessObjects.Enum;

namespace BusinessObjects.DataTransferObject
{
    public class RecordDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TypeRecord Type { get; set; }

        public ICollection<ArtistDto> Artists { get; set; }
        public ICollection<TrackDto> Tracks { get; set; }
    }
}