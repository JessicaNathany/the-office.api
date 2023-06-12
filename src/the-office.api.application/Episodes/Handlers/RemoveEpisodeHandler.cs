using the_office.api.application.Common.Commands;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.application.Episodes.Messaging.Responses;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Episodes.Handlers;

public class RemoveEpisodeHandler : ICommandHandler<RemoveEpisodeRequest>
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveEpisodeHandler(IEpisodeRepository episodeRepository, IUnitOfWork unitOfWork)
    {
        _episodeRepository = episodeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveEpisodeRequest request, CancellationToken cancellationToken)
    {
        var episode = await _episodeRepository.GetById(request.Id, cancellationToken);
        if (episode is null)
            return Result.Failure<EpisodeResponse>(EpisodeError.NotFound);

        _episodeRepository.Remove(episode);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}