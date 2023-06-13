using the_office.api.application.Common.Commands;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.api.application.Seasons.Messaging.Responses;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Seasons.Handlers;

internal sealed class SeasonHandler : ICommandHandler<SeasonRequest, List<SeasonResponse>>
{
    private readonly ISeasonRepository _seasonRepository;

    public SeasonHandler(ISeasonRepository seasonRepository)
    {
        _seasonRepository = seasonRepository;
    }

    // to implement interface repository
    public Task<Result<List<SeasonResponse>>> Handle(SeasonRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}