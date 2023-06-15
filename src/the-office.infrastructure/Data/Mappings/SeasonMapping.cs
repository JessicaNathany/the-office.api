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

        builder.Property(b => b.Number)
            .IsRequired();

        builder.Property(b => b.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.TotalEpisodes)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(b => b.Summary)
            .HasMaxLength(750)
            .IsRequired();

        builder.Property(b => b.ReleaseDate)
            .HasColumnType("date")
            .IsRequired();

        builder.HasMany(season => season.Episodes)
            .WithOne(episode => episode.Season)
            .HasForeignKey(episode => episode.SeasonId);

        builder.HasMany<Character>()
            .WithMany();

        builder.ToTable("Season");
    }
}