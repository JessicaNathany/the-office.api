using the_office.domain.Model;

namespace the_office.domain.Domain
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
    }
}
