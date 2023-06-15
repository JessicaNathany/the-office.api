using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using the_office.api.application.Common.Commands;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.api.application.Seasons.Messaging.Responses;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Seasons.Handlers;

public sealed class GetSeasonByIdHandler : ICommandHandler<GetSeasonByIdRequest, SeasonResponse>
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IMapper _mapper;

    public GetSeasonByIdHandler(ISeasonRepository seasonRepository, IMapper mapper)
    {
        _seasonRepository = seasonRepository;
        _mapper = mapper;
    }

    public async Task<Result<SeasonResponse>> Handle(GetSeasonByIdRequest request, CancellationToken cancellationToken = default)
    {
        var season = await _seasonRepository.GetQueryable()
            .AsNoTracking()
            .ProjectTo<SeasonResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(season => season.Id == request.Id, cancellationToken);

        return season ?? Result.Failure<SeasonResponse>(SeasonError.NotFound);
    }
}