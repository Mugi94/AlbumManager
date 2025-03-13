using BusinessObjects.Enum;

namespace BusinessObjects.Entity
{
    public class Record: IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TypeRecord Type { get; set; }
        public IEnumerable<Artist> Artists { get; set; }
        public IEnumerable<Track> Tracks { get; set; }

        public Record()
        {
        }

        public Record(int id, string title, DateTime releaseDate, TypeRecord type, IEnumerable<Artist> artists, IEnumerable<Track> tracks)
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