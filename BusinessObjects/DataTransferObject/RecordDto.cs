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

        public RecordDto(int id, string title, DateTime releaseDate, TypeRecord type, ICollection<ArtistDto> artists, ICollection<TrackDto> tracks)
        {
            Id = id;
            Title = title;
            ReleaseDate = releaseDate;
            Type = type;
            Artists = artists;
            Tracks = tracks;
        }
    }
}