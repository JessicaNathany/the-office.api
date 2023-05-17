namespace the_office.domain.Entities
{
    /// <summary>
    /// Relationship interim table between Episode and Character type many-to-many
    /// </summary>
    public class EpisodeCharacter
    {
        public int Id { get; set; }

        public int IdCharacter { get; set; }

        public Character Character { get; set; }

        public int IdEpisode { get; set; }

        public Episode Episode { get; set; }
    }
}
