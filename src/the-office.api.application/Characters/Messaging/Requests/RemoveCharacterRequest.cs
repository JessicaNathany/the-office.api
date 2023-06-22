using the_office.api.application.Common.Commands;

namespace the_office.api.application.Characters.Messaging.Requests
{
    public sealed record RemoveCharacterRequest(int Id) : ICommand;
}
