using AutoMapper;
using the_office.api.application.Episodes.Requests;
using the_office.api.application.Episodes.Responses;
using the_office.domain.Repositories;
using the_office.insfrastructure.Mediator.Message;

namespace the_office.api.application.Episodes.Handlers;

public class GetEpisodeByIdHandler : CommandHandler<GetEpisodeByIdRequest, EpisodeResponse>
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IMapper _mapper;

    public GetEpisodeByIdHandler(IEpisodeRepository episodeRepository, IMapper mapper)
    {
        _episodeRepository = episodeRepository;
        _mapper = mapper;
    }

    public override async Task<CommandResponse<EpisodeResponse>> Handle(GetEpisodeByIdRequest request, CancellationToken cancellationToken)
    {
        var episode = await _episodeRepository.Get(request.Id);

        // TODO: Need to refactor this for an appropriate result
        if (episode is null)
            return new CommandResponse<EpisodeResponse>(null, null, null);

        var response = _mapper.Map<EpisodeResponse>(episode);

        // TODO: Need simplify this CommandResponse creation
        return new CommandResponse<EpisodeResponse>(response, null, null);
    }
}