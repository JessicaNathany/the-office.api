using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using the_office.api.application.Common.Commands;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.application.Episodes.Messaging.Responses;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Episodes.Handlers;

public sealed class GetEpisodeByIdHandler : ICommandHandler<GetEpisodeByIdRequest, EpisodeResponse>
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IMapper _mapper;

    public GetEpisodeByIdHandler(IEpisodeRepository episodeRepository, IMapper mapper)
    {
        _episodeRepository = episodeRepository;
        _mapper = mapper;
    }

    public async Task<Result<EpisodeResponse>> Handle(GetEpisodeByIdRequest request, CancellationToken cancellationToken = default)
    {
        var episode = await _episodeRepository.GetQueryable()
            .AsNoTracking()
            .Include(episode => episode.Characters)
            .Include(episode => episode.Season)
            .ProjectTo<EpisodeResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(episode => episode.Id == request.Id, cancellationToken: cancellationToken);

        return episode ?? Result.Failure<EpisodeResponse>(EpisodeError.NotFound);
    }
}