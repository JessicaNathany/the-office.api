using AutoMapper;
using the_office.api.application.Common.Commands;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.application.Episodes.Messaging.Responses;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Episodes.Handlers;

internal sealed class GetEpisodeByIdHandler : ICommandHandler<GetEpisodeByIdRequest, EpisodeResponse>
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IMapper _mapper;

    public GetEpisodeByIdHandler(IEpisodeRepository episodeRepository, IMapper mapper)
    {
        _episodeRepository = episodeRepository;
        _mapper = mapper;
    }

    public async Task<Result<EpisodeResponse>> Handle(GetEpisodeByIdRequest request, CancellationToken cancellationToken)
    {
        var episode = await _episodeRepository.Get(request.Id);

        if (episode is null)
            return Result.Failure<EpisodeResponse>(EpisodeError.NotFound);

        var response = _mapper.Map<EpisodeResponse>(episode);

        return response;
    }
}