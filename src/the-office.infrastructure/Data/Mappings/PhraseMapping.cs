using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the_office.domain.Entities;

namespace the_office.infrastructure.Data.Mappings
{
    public class PhrasesMapping : IEntityTypeConfiguration<Phrases>
    {
        public void Configure(EntityTypeBuilder<Phrases> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Phrase)
                .HasColumnType("varchar(500)");


            builder.Property(p => p.CharacterName)
                .HasColumnType("varchar(50)");

            builder.ToTable("Phrase");
        }
    }
}
