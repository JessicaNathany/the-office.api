using the_office.api.application.Common.Commands;
using the_office.api.application.Season.Messaging.Responses;

namespace the_office.api.application.Season.Messaging.Requests
{
    public sealed record SeasonRequest() : ICommand<List<SeasonResponse>>
    {
    }
}
