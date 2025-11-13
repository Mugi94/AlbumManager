namespace BusinessObjects.DataTransferObject
{
    public class TrackDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }

        public ICollection<ArtistDto> Artists { get; set; }

        public TrackDto(int id, string title, int duration, ICollection<ArtistDto> artists)
        {
            Id = id;
            Title = title;
            Duration = duration;
            Artists = artists;
        }
    }
}