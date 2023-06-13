using the_office.api.application.Common.Commands;

namespace the_office.api.application.Episodes.Messaging.Requests;

public sealed record RemoveEpisodeRequest(int Id) : ICommand;