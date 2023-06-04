using the_office.domain.Shared;

namespace the_office.domain.Entities;

public class Phrases : Entity
{
    /// <summary>
    /// Phrase
    /// </summary>
    public string Phrase { get; set; }

    /// <summary>
    /// Name of the Character in the phrase
    /// </summary>
    public string CharacterName { get; set; }
}