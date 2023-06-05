using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the_office.domain.Entities;

namespace the_office.infrastructure.Data.Mappings;

public class EpisodeMapping : IEntityTypeConfiguration<Episode>
{
    public void Configure(EntityTypeBuilder<Episode> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.HasIndex(b => b.Code)
            .IsUnique();

        builder.Property(b => b.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.AirDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(b => b.Description)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(episode => episode.Season)
            .WithMany(season => season.Episodes)
            .HasForeignKey(b => b.SeasonId)
            .IsRequired();

        builder.HasMany<Character>(episode => episode.Characters)
            .WithMany(e => e.Episodes);

        builder.ToTable("Episode");
    }
}