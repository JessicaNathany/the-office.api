using the_office.domain.Shared;

namespace the_office.domain.Entities;

public class Episode : Entity
{
    public Episode(string name, string description, DateTime airDate, Season season)
    {
        Name = name;
        Description = description;
        AirDate = airDate;
        Season = season;
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

    private readonly List<Character> _characters = new();

    /// <summary>
    /// The characters seen in this episode.
    /// </summary>
    public IEnumerable<Character> Characters => _characters.AsReadOnly();

    /// <summary>
    /// Id the Season episode.
    /// </summary>
    public int SeasonId { get; set; }

    /// <summary>
    /// The season seen in this episode.
    /// </summary>
    public Season Season { get; set; }

    public void AddCharacters(IEnumerable<Character> characters) => _characters.AddRange(characters);
    
    public void ChangeInfo(string name, string description, DateTime airDate, Season season)
    {
        Name = name;
        Description = description;
        AirDate = airDate;
        Season = season;
    }

    public void ChangeCharacters(IEnumerable<Character> characters)
    {
        _characters.Clear();
        _characters.AddRange(characters);
    }

    public void RemoveAllCharacters()
    {
        if(!_characters.Any())
            return;
        
        _characters.Clear();
    }
}