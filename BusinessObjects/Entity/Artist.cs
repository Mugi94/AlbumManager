namespace BusinessObjects.Entity
{
    public class Artist: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DebutYear { get; set; }
        public IEnumerable<Record> Records { get; set; }
        public IEnumerable<Track> Tracks { get; set; }

        public Artist()
        {   
        }

        public Artist(int id, string name, int debutYear, IEnumerable<Record> records, IEnumerable<Track> tracks)
        {
            Id = id;
            Name = name;
            DebutYear = debutYear;
            Records = records;
            Tracks = tracks;
        }
    }
}