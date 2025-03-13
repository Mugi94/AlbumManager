using BusinessObjects.Entity;
using DataAccessLayer.Repository;

namespace AlbumManager.App
{
    class Program
    {
        static void Main(string[] args)
        {
            ArtistRepository artistRepository = new();

            IEnumerable<Artist> artists = artistRepository.GetAll();

            var recentArtists = artists.Where(a => a.DebutYear >= 2020).ToList();
            foreach (var artist in recentArtists)
            {
                Console.WriteLine($"{artist.Name} = {artist.DebutYear}");
            }
        }
    }
}