using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.Contexts;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
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
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped<IGenericRepository<Artist>, ArtistRepository>();
                    services.AddScoped<IGenericRepository<Record>, RecordRepository>();
                    services.AddScoped<IGenericRepository<Track>, TrackRepository>();

                    services.AddScoped<IArtistManager, ArtistManager>();
                    services.AddScoped<IRecordManager, RecordManager>();
                    services.AddScoped<ITrackManager, TrackManager>();

                    services.AddDbContext<DiscographyContext>(options =>
                        options.UseNpgsql("Host=localhost;Port=5432;Database=AlbumManagerDb;Username=postgres;Password=root")
                    );
                })
                .Build();
        }
    }
}