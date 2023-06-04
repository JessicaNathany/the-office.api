using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the_office.domain.Entities;

namespace the_office.infrastructure.Data.Mappings;

public class CharacterMapping : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(c => c.Code).IsUnique();

        builder.Property(b => b.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.NameActor)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.Status)
            .IsRequired();

        builder.Property(b => b.Gender)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.Job)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.ImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasMany(character => character.Episodes)
            .WithMany(episode => episode.Characters);
        
        builder.ToTable("Character");

    }
}