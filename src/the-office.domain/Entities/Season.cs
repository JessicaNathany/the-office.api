using the_office.domain.Shared;

namespace the_office.domain.Entities;

public class Season : Entity
{
    public Season(string description)
    {
        Description = description;
    }
        
    /// <summary>
    /// Description of the Season
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// List episodes of the Season
    /// </summary>
    public IEnumerable<Episode> Episodes { get; set; }

    /// <summary>
    /// List characters of the Season
    /// </summary>
    public IEnumerable<Character> Characters { get; set; }
}