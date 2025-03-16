using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Contexts
{
    public class DiscographyContext: DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Track> Tracks { get; set; }
        
        public DiscographyContext(DbContextOptions<DiscographyContext> options) : base(options)
        {
        }
    }
}