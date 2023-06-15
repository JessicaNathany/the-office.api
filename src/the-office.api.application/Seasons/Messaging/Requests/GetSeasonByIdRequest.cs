using the_office.api.application.Common.Commands;
using the_office.api.application.Seasons.Messaging.Responses;

namespace the_office.api.application.Seasons.Messaging.Requests;

public record GetSeasonByIdRequest(int Id) : ICommand<SeasonResponse>;