using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the_office.domain.Domain;

namespace the_office.insfrastructure.Mappings
{
    public class EpisodeCharacterMapping : IEntityTypeConfiguration<EpisodeCharacter>
    {
        public void Configure(EntityTypeBuilder<EpisodeCharacter> builder)
        {
            builder.HasKey(ec => ec.Id);

            // continua
        }
    }
}
