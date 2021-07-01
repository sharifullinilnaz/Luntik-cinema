using Microsoft.EntityFrameworkCore;

namespace SeanceMicroservice.Models
{
    public class SeanceContext : DbContext
    {
        public SeanceContext(DbContextOptions<SeanceContext> options)
            : base(options)
        {
        }

        public DbSet<Seance> Seances { get; set; }
    }
}