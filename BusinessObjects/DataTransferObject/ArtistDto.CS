namespace BusinessObjects.DataTransferObject
{
    public class ArtistDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DebutYear { get; set; }

        public ArtistDto(int id, string name, int debutYear)
        {
            Id = id;
            Name = name;
            DebutYear = debutYear;
        }
    }
}