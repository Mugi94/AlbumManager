using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Contexts
{
    public class MusicContext: DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Track> Tracks { get; set; }
        
        public MusicContext(DbContextOptions<MusicContext> options) : base(options)
        {
        }
    }
}