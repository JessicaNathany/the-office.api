using Microsoft.EntityFrameworkCore;
using the_office.domain.Entities;
using the_office.domain.Shared;

namespace the_office.infrastructure.Data.Context;

public class TheOfficeDbContext : DbContext
{
    public TheOfficeDbContext(DbContextOptions<TheOfficeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Character> Characters => Set<Character>();

    public DbSet<Episode> Episodes => Set<Episode>();

    public DbSet<Phrases> Phrases => Set<Phrases>();

    public DbSet<Season> Seasons => Set<Season>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheOfficeDbContext).Assembly);
        
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        
        base.OnModelCreating(modelBuilder);
    }
}