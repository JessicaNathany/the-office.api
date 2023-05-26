using the_office.api.application.Common.Commands;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.application.Episodes.Messaging.Responses;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Episodes.Handlers;

internal sealed class EpisodeHandler : ICommandHandler<EpisodeRequest, List<EpisodeResponse>>
{
    private readonly IEpisodeRepository _episodeRepository;

    public EpisodeHandler(IEpisodeRepository episodeRepository)
    {
        _episodeRepository = episodeRepository;
    }
    
    public Task<Result<List<EpisodeResponse>>> Handle(EpisodeRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}