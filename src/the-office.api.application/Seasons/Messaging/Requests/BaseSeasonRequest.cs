using the_office.api.application.Common.Commands;
using the_office.api.application.Seasons.Messaging.Responses;

namespace the_office.api.application.Seasons.Messaging.Requests;

public class BaseSeasonRequest : ICommand<SeasonResponse>
{
    public int Number { get; set; }
    public string Title { get; set; }
    public int TotalEpisodes { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Summary { get; set; }
}