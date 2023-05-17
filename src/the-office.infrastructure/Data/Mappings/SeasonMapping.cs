using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the_office.domain.Entities;

namespace the_office.insfrastructure.Mappings
{
    public class SeasonMapping : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Season");
        }
    }
}
