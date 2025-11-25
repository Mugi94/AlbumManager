namespace BusinessObjects.DataTransferObject
{
    public class TrackDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Duration { get; set; }

        public ICollection<ArtistDto> Artists { get; set; } = new List<ArtistDto>();
    }
}