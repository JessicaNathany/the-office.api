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

            builder.HasOne(ep => ep.Episode)
                .WithMany(et => et.Characters)
                .HasForeignKey(e => e.IdEpisode);

            builder.HasOne(ep => ep.Character)
                .WithMany(et => et.Episodes)
                .HasForeignKey(e => e.IdCharacter);

            builder.ToTable("EpisodeCharacter");
        }
    }
}
