using the_office.api.application.Common.Commands;
using the_office.api.application.Episodes.Messaging.Responses;

namespace the_office.api.application.Episodes.Messaging.Requests;

public sealed record RegisterEpisodeRequest : ICommand<EpisodeResponse>
{
    public string Name { get; set; }
    public DateTime AirDate { get; set; }
    public string Description { get; set; }
    public Guid SeasonCode { get; set; }
    public IEnumerable<Guid>? Characters { get; set; }
    public bool HasCharacters => Characters is not null && Characters.Any();
}