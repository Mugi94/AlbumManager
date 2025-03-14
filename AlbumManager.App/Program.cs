using BusinessObjects.Entity;
using BusinessObjects.Enum;
using Services.Services;

namespace AlbumManager.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- (ArtistManager) ---");
            ArtistManager artistManager = new();
            IEnumerable<Artist> artists = artistManager.GetArtists();
            foreach (var artist in artists)
            {
                Console.WriteLine($"{artist.Name} - {artist.DebutYear}");
            }

            Console.WriteLine("\n--- (RecordManager) ---");
            RecordManager recordManager = new();
            IEnumerable<Record> records = recordManager.GetRecords();
            foreach (var record in records)
            {
                Console.WriteLine($"{record.Title} - {record.Type}");
            }

            Console.WriteLine("\n--- (TrackManager) ---");
            TrackManager trackManager = new();
            IEnumerable<Track> tracks = trackManager.GetTracks();
            foreach (var track in tracks)
            {
                Console.WriteLine($"{track.Title} - {track.Duration}s");
            }
        }
    }
}