using the_office.api.application.Common.Mappings;
using the_office.api.application.Episodes.Messaging.Responses;
using the_office.domain.Entities;

namespace the_office.api.application.Characters.Messaging.Response;

public class CharacterResponse : IMapFrom<Character>
{
    public string Name { get; set; }

    public string NameActor { get; set; }

    public bool Status { get; set; }

    public string Gender { get; set; }

    public string ImageUrl { get; set; }
    public string Job { get; set; }
}