using Microsoft.EntityFrameworkCore;
using the_office.domain.Domain;

namespace the_office.insfrastructure.Context
{
    public class TheOfficedbContext : DbContext
    {
        public TheOfficedbContext(DbContextOptions<TheOfficedbContext> options) 
            : base(options)
        { }

        public DbSet<Character> Characters { get; set; }

        public DbSet<Episode> Episodes { get; set; }

        public DbSet<Phrases> Phrases { get; set; }

        public DbSet<Season> Seasons { get; set; }

    }
}
