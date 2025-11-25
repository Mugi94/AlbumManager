namespace BusinessObjects.DataTransferObject
{
    public class TrackRequest
    {
        public string Title { get; set; } = string.Empty;
        public int Duration { get; set; }

        public ICollection<ArtistSummary> Artists { get; set; } = new List<ArtistSummary>();
    }
}