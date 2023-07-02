using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;

namespace the_office.api.application.Characters.Messaging.Requests
{
    public sealed record UpdateCharacterRequest : ICommand<CharacterResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string NameActor { get; set; }

        public bool Status { get; set; }

        public string Gender { get; set; }

        public string ImageUrl { get; set; }

        public string Job { get; set; }

        public IEnumerable<Guid> Episodes { get; set; }
    }
}
