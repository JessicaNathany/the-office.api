using the_office.api.application.Common.Mappings;
using the_office.domain.Entities;

namespace the_office.api.application.Episodes.Messaging.Responses;

public class EpisodeResponse : IMapFrom<Episode>
{
    public string Name { get; set; }
    public string AirDate { get; set; }
    public string Description { get; set; }
}