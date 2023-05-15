using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the_office.domain.Domain;

namespace the_office.insfrastructure.Mappings
{
    public class EpisodeMapping : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(b => b.AirDate)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(b => b.Description)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(b => b.SeasonId)
               .IsRequired()
               .HasColumnType("int");

            builder.ToTable("Episode");
        }
    }
}
