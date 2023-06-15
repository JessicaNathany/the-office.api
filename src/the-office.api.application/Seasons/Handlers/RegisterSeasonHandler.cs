using AutoMapper;
using the_office.api.application.Common.Commands;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.api.application.Seasons.Messaging.Responses;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Seasons.Handlers;

public sealed class RegisterSeasonHandler : ICommandHandler<RegisterSeasonRequest, SeasonResponse>
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterSeasonHandler(ISeasonRepository seasonRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _seasonRepository = seasonRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<SeasonResponse>> Handle(RegisterSeasonRequest request, CancellationToken cancellationToken = default)
    {
        var exists = await _seasonRepository.Any(season => season.Number == request.Number, cancellationToken);
        if (exists)
            return Result.Failure<SeasonResponse>(SeasonError.AlreadyExists);

        var season = new Season(request.Number, request.Title, request.TotalEpisodes, request.ReleaseDate, request.Summary);
        
        _seasonRepository.Add(season);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SeasonResponse>(season);
    }
}