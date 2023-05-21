using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using the_office.domain.Entities;

namespace the_office.infrastructure.Data.Context
{
    public class TheOfficeDbContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public TheOfficeDbContext(DbContextOptions<TheOfficeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }

        public DbSet<Episode> Episodes { get; set; }

        public DbSet<Phrases> Phrases { get; set; }

        public DbSet<Season> Seasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheOfficeDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            base.OnModelCreating(modelBuilder);
        }
    }
}