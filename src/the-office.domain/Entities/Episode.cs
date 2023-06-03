using the_office.domain.Shared;

namespace the_office.domain.Entities
{
    public sealed class Episode : Entity
    {
        protected Episode() { }
        
        public Episode(string name, string description, DateTime airDate, int seasonId)
        {
            Name = name;
            Description = description;
            AirDate = airDate;
            SeasonId = seasonId;
        }

        /// <summary>
        /// The name of the episode
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// When this Episode aired 
        /// </summary>
        public DateTime AirDate { get; set; }

        /// <summary>
        /// The description of the episode
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The characters seen in this episode.
        /// </summary>
        public IEnumerable<EpisodeCharacter> Characters { get; set; }

         /// <summary>
        /// Id the Season episode.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// The season seen in this episode.
        /// </summary>
        public Season Season { get; set; }
    }
}
