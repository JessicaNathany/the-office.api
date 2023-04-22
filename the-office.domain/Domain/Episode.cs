using Newtonsoft.Json;
using the_office.domain.Model;

namespace the_office.domain.Domain
{
    public sealed class Episode : Entity
    {
        /// <summary>
        /// The name of the episode
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// When this Episode aired 
        /// </summary>
        [JsonProperty("air_date")]
        public string AirDate { get; set; }

        /// <summary>
        /// The description of the episode
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The characters seen in this episode.
        /// </summary>
        public string[] Characters { get; set; }

        /// <summary>
        /// Id the Season episode.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Object Season 
        /// </summary>
        public Season Season { get; set; }
    }
}
