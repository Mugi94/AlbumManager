using BusinessObjects.Entity;
using BusinessObjects.Enum;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>()
                .Property(e => e.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (TypeRecord)Enum.Parse(typeof(TypeRecord), v)
                );
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}