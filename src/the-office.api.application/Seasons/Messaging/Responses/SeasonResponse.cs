using the_office.api.application.Common.Mappings;
using the_office.domain.Entities;

namespace the_office.api.application.Seasons.Messaging.Responses;

public record SeasonResponse : IMapFrom<Season>
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public int TotalEpisodes { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Summary { get; set; }
}