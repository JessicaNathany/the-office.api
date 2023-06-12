using AutoMapper;
using the_office.api.application.Common.Commands;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.application.Episodes.Messaging.Responses;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Episodes.Handlers;

public sealed class UpdateEpisodeHandler : ICommandHandler<UpdateEpisodeRequest, EpisodeResponse>
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly ISeasonRepository _seasonRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEpisodeHandler(IEpisodeRepository episodeRepository, ISeasonRepository seasonRepository,
        ICharacterRepository characterRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _episodeRepository = episodeRepository;
        _seasonRepository = seasonRepository;
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<EpisodeResponse>> Handle(UpdateEpisodeRequest request, CancellationToken cancellationToken = default)
    {
        var episode = await _episodeRepository.GetById(request.Id, cancellationToken);
        if (episode is null)
            return Result.Failure<EpisodeResponse>(EpisodeError.NotFound);
        
        var season = await _seasonRepository.Get(season => season.Code == request.SeasonCode, cancellationToken);
        if (season is null)
            return Result.Failure<EpisodeResponse>(EpisodeError.SeasonNotValid);

        episode.ChangeInfo(request.Name, request.Description, request.AirDate, season);
        
        if (request.HasCharacters)
        {
            var characters = await _characterRepository.GetAll(character => request.Characters!.Contains(character.Code), cancellationToken);
            
            if(characters is null || characters.Count != request.Characters!.Count())
                return Result.Failure<EpisodeResponse>(EpisodeError.CharactersNotValid);
            
            episode.ChangeCharacters(characters);
        }
        else
        {
            episode.RemoveAllCharacters();
        }
        
        _episodeRepository.Update(episode);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<EpisodeResponse>(episode);
    }
}