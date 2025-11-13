using BusinessObjects.Enum;

namespace BusinessObjects.Entity
{
    public class Record: IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TypeRecord Type { get; set; }

        public ICollection<Artist> Artists { get; set; } = new List<Artist>();
        public ICollection<Track> Tracks { get; set; } = new List<Track>();

        public Record(int id, string title, DateTime releaseDate, TypeRecord type, ICollection<Artist> artists, ICollection<Track> tracks)
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