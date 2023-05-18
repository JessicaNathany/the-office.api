using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the_office.domain.Entities;

namespace the_office.insfrastructure.Mappings
{
    public class CharacterMapping : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(b => b.Name)  
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(b => b.NameActor)
               .IsRequired()
               .HasColumnType("varchar(50)");

            builder.Property(b => b.Status)
               .IsRequired()
               .HasColumnType("boolean");

            builder.Property(b => b.Gender)
               .IsRequired()
               .HasColumnType("varchar(50)");

            builder.Property(b => b.Job)
              .IsRequired()
              .HasColumnType("varchar(50)");

            builder.Property(b => b.ImageUrl)
               .IsRequired()
               .HasColumnType("varchar(500)");

            builder.ToTable("Character");
        }
    }
}
