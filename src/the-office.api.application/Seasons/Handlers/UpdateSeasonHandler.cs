using AutoMapper;
using the_office.api.application.Common.Commands;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.api.application.Seasons.Messaging.Responses;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Seasons.Handlers;

public sealed class UpdateSeasonHandler : ICommandHandler<UpdateSeasonRequest, SeasonResponse>
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSeasonHandler(ISeasonRepository seasonRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _seasonRepository = seasonRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Result<SeasonResponse>> Handle(UpdateSeasonRequest request, CancellationToken cancellationToken = default)
    {
        var season = await _seasonRepository.GetById(request.Id, cancellationToken);
        if (season is null)
            return Result.Failure<SeasonResponse>(SeasonError.NotFound);

        season.ChangeInfo(request.Number, request.Title, request.TotalEpisodes, request.ReleaseDate, request.Summary);
        
        _seasonRepository.Update(season);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SeasonResponse>(season);
    }
}