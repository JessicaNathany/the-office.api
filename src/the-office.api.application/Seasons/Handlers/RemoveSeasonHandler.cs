using the_office.api.application.Common.Commands;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.api.application.Seasons.Messaging.Responses;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Seasons.Handlers;

public sealed class RemoveSeasonHandler : ICommandHandler<RemoveSeasonRequest>
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSeasonHandler(ISeasonRepository seasonRepository, IUnitOfWork unitOfWork)
    {
        _seasonRepository = seasonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveSeasonRequest request, CancellationToken cancellationToken = default)
    {
        var season = await _seasonRepository.GetById(request.Id, cancellationToken);
        if (season is null)
            return Result.Failure<SeasonResponse>(SeasonError.NotFound);

        _seasonRepository.Remove(season);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}