using AutoMapper;
using the_office.api.application.Common.Commands;
using the_office.api.application.Common.Mappings;
using the_office.api.application.Common.Pagination;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.application.Episodes.Messaging.Responses;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Episodes.Handlers;

public sealed class GetEpisodesHandler : ICommandHandler<GetEpisodesRequest, PagedResult<EpisodeResponse>>
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IMapper _mapper;

    public GetEpisodesHandler(IEpisodeRepository episodeRepository, IMapper mapper)
    {
        _episodeRepository = episodeRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<EpisodeResponse>>> Handle(GetEpisodesRequest request, CancellationToken cancellationToken = default)
    {
        var episodes = await _episodeRepository.GetAll(request.Page, request.PageSize, cancellationToken);

        return _mapper.MapToPagedResult<EpisodeResponse>(episodes);
    }
}