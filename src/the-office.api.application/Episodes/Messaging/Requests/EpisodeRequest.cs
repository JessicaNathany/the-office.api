using the_office.api.application.Common.Commands;
using the_office.api.application.Episodes.Messaging.Responses;

namespace the_office.api.application.Episodes.Messaging.Requests;

public sealed record EpisodeRequest : ICommand<List<EpisodeResponse>>
{
}