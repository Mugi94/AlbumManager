using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Services;

namespace AlbumManager.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder();
            var artistManager = host.Services.GetService<IArtistManager>();
            var recordManager = host.Services.GetService<IRecordManager>();
            var trackManager = host.Services.GetService<ITrackManager>();

            Console.WriteLine("--- (ArtistManager) ---");
            IEnumerable<Artist> artists = artistManager.GetArtists();
            foreach (var artist in artists)
            {
                Console.WriteLine($"{artist.Name} - {artist.DebutYear}");
            }

            Console.WriteLine("\n--- (RecordManager) ---");
            IEnumerable<Record> records = recordManager.GetRecords();
            foreach (var record in records)
            {
                Console.WriteLine($"{record.Title} - {record.Type}");
            }

            Console.WriteLine("\n--- (TrackManager) ---");
            IEnumerable<Track> tracks = trackManager.GetTracks();
            foreach (var track in tracks)
            {
                Console.WriteLine($"{track.Title} - {track.Duration}s");
            }
        }
        
        private static IHost CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IGenericRepository<Artist>, ArtistRepository>();
                    services.AddSingleton<IGenericRepository<Record>, RecordRepository>();
                    services.AddSingleton<IGenericRepository<Track>, TrackRepository>();

                    services.AddSingleton<IArtistManager, ArtistManager>();
                    services.AddSingleton<IRecordManager, RecordManager>();
                    services.AddSingleton<ITrackManager, TrackManager>();
                })
                .Build();
        }
    }
}