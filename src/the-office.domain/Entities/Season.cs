using the_office.domain.Common;

namespace the_office.domain.Entities
{
    public class Season : Entity
    {
        /// <summary>
        /// Description of the Season
        /// </summary>
        public int Description { get; set; }

        /// <summary>
        /// List episodes of the Seeason
        /// </summary>
        public List<Episode> Episodes { get; set; }

        /// <summary>
        /// List characters of the Seeason
        /// </summary>
        public List<Character> Characters { get; set; }
    }
}
