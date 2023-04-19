namespace the_office.domain.Domain
{
    public sealed class Season
    {
        /// <summary>
        /// Id of the Season
        /// </summary>
        public int Id { get; set; }

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
