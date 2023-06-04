using AutoMapper;
using the_office.api.application.Common.Commands;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.application.Episodes.Messaging.Responses;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Episodes.Handlers;

internal sealed class RegisterEpisodeHandler : ICommandHandler<RegisterEpisodeRequest, EpisodeResponse>
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly ISeasonRepository _seasonRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterEpisodeHandler(IEpisodeRepository episodeRepository, ISeasonRepository seasonRepository, ICharacterRepository characterRepository,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _episodeRepository = episodeRepository;
        _seasonRepository = seasonRepository;
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<EpisodeResponse>> Handle(RegisterEpisodeRequest request, CancellationToken cancellationToken)
    {
        var season = await _seasonRepository.GetByCode(request.SeasonCode, cancellationToken);

        if (season is null)
            return Result.Failure<EpisodeResponse>(EpisodeError.SeasonNotValid);
        
        var episode = new Episode(request.Name, request.Description, request.AirDate, season);

        if (request.HasCharacters)
        {
            var characters = await _characterRepository.GetAll(c => request.Characters!.Contains(c.Code), cancellationToken);
            
            if(characters is null || characters.Count() != request.Characters!.Count())
                return Result.Failure<EpisodeResponse>(EpisodeError.CharactersNotValid);
            
            episode.AddCharacters(characters);
        }

        _episodeRepository.Add(episode);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<EpisodeResponse>(episode);
    }
}