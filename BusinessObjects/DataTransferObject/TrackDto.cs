namespace BusinessObjects.DataTransferObject
{
    public class TrackDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }

        public ICollection<ArtistDto> Artists { get; set; }
    }
}