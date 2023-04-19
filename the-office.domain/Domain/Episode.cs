﻿using Newtonsoft.Json;

namespace the_office.domain.Domain
{
    public sealed class Episode
    {
        /// <summary>
        /// Id to find the Episode
        /// </summary>
        public int Id { get; set; }

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
