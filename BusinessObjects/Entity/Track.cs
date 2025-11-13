namespace BusinessObjects.Entity
{
    public class Track: IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }

        public ICollection<Artist> Artists { get; set; } = new List<Artist>();
        public ICollection<Record> Records { get; set; } = new List<Record>();

        public Track(int id, string title, int duration, ICollection<Artist> artists, ICollection<Record> records)
        {
            Id = id;
            Title = title;
            Duration = duration;
            Artists = artists;
            Records = records;
        }
    }
}