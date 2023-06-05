using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the_office.domain.Entities;

namespace the_office.infrastructure.Data.Mappings;

public class SeasonMapping : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.HasIndex(b => b.Code)
            .IsUnique();

        builder.Property(b => b.Description)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(season => season.Episodes)
            .WithOne(episode => episode.Season)
            .HasForeignKey(episode => episode.SeasonId);

        builder.HasMany<Character>()
            .WithMany();

        builder.ToTable("Season");
    }
}