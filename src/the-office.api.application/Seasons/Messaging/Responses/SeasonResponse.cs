using the_office.api.application.Common.Mappings;
using the_office.domain.Entities;

namespace the_office.api.application.Seasons.Messaging.Responses;

public record SeasonResponse : IMapFrom<Season>
{
    public int Id { get; set; }
    public Guid Code { get; set; }
    public string Description { get; set; }
}