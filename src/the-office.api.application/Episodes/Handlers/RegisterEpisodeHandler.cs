using AutoMapper;
using the_office.api.application.Common.Commands;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.application.Episodes.Messaging.Responses;
using the_office.domain.Entities;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Episodes.Handlers;

internal sealed class RegisterEpisodeHandler : ICommandHandler<RegisterEpisodeRequest, EpisodeResponse>
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterEpisodeHandler(IEpisodeRepository episodeRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _episodeRepository = episodeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<EpisodeResponse>> Handle(RegisterEpisodeRequest request, CancellationToken cancellationToken)
    {
        var episode = new Episode(request.Name, request.Description, request.AirDate, request.SeasonId);

        _episodeRepository.Add(episode);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<EpisodeResponse>(episode);
    }
}