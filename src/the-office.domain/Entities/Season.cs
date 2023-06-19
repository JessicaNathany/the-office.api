using the_office.domain.Shared;

namespace the_office.domain.Entities;

public class Season : Entity
{
    public Season(int number, string title, int totalEpisodes, DateTime releaseDate, string summary)
    {
        Number = number;
        Title = title;
        TotalEpisodes = totalEpisodes;
        ReleaseDate = releaseDate;
        Summary = summary;
    }

    /// <summary>
    /// Season number
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Season title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Number of episodes in the season
    /// </summary>
    public int TotalEpisodes { get; set; }

    /// <summary>
    /// Release date of the season
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Description or summary of the season
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// List episodes in the season
    /// </summary>
    public IEnumerable<Episode> Episodes { get; set; }

    /// <summary>
    /// List characters in the season
    /// </summary>
    public IEnumerable<Character> Characters { get; set; }

    public void ChangeInfo(int number, string title, int totalEpisodes, DateTime releaseDate, string summary)
    {
        Number = number;
        Title = title;
        TotalEpisodes = totalEpisodes;
        ReleaseDate = releaseDate;
        Summary = summary;
    }
}