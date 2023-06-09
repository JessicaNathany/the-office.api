using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the_office.domain.Entities;

namespace the_office.infrastructure.Data.Mappings;

public class PhrasesMapping : IEntityTypeConfiguration<Phrases>
{
    public void Configure(EntityTypeBuilder<Phrases> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.HasIndex(b => b.Code)
            .IsUnique();

        builder.Property(p => p.Phrase)
            .HasMaxLength(500);

        builder.Property(p => p.CharacterName)
            .HasMaxLength(50);

        builder.ToTable("Phrase");
    }
}