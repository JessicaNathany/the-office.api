using the_office.api.application.Common.Commands;
using the_office.api.application.Episodes.Messaging.Responses;

namespace the_office.api.application.Episodes.Messaging.Requests;

public sealed record RegisterEpisodeRequest : ICommand<EpisodeResponse>
{
    public string Name { get; set; }
    public DateTime AirDate { get; set; }
    public string Description { get; set; }
    public int SeasonNumber { get; set; }
    public List<int>? CharacterIds { get; set; }
    public bool HasCharacters => CharacterIds is not null && CharacterIds.Any();
}