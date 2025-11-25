namespace BusinessObjects.DataTransferObject
{
    public class ArtistResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DebutYear { get; set; }

        public ICollection<RecordSummary> Records { get; set; } = new List<RecordSummary>();
    }
}