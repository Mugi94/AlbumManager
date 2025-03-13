namespace BusinessObjects.Entity
{
    public class Artist: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DebutYear { get; set; }

        public Artist()
        {   
        }

        public Artist(int id, string name, int debutYear)
        {
            Id = id;
            Name = name;
            DebutYear = debutYear;
        }
    }
}