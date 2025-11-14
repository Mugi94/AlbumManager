namespace BusinessObjects.Entity
{
    public class Artist: IEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DebutYear { get; set; }

        public ICollection<Record> Records { get; set; } = new List<Record>();
        public ICollection<Track> Tracks { get; set; } = new List<Track>();

        public Artist()
        {
        }

        public Artist(int id, string name, int debutYear, ICollection<Record> records, ICollection<Track> tracks)
        {
            Id = id;
            Name = name;
            DebutYear = debutYear;
            Records = records;
            Tracks = tracks;
        }
    }
}