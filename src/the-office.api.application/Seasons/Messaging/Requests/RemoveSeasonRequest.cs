using the_office.api.application.Common.Commands;

namespace the_office.api.application.Seasons.Messaging.Requests;

public record RemoveSeasonRequest(int Id) : ICommand;