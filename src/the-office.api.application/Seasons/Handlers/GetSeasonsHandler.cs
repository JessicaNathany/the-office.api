using AutoMapper;
using the_office.api.application.Common.Commands;
using the_office.api.application.Common.Mappings;
using the_office.api.application.Common.Pagination;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.api.application.Seasons.Messaging.Responses;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Seasons.Handlers;

public sealed class GetSeasonsHandler: ICommandHandler<GetSeasonsRequest, PagedResult<SeasonResponse>>
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IMapper _mapper;

    public GetSeasonsHandler(ISeasonRepository seasonRepository, IMapper mapper)
    {
        _seasonRepository = seasonRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<SeasonResponse>>> Handle(GetSeasonsRequest request, CancellationToken cancellationToken = default)
    {
        var seasons = await _seasonRepository.GetAll(request.Page, request.PageSize, cancellationToken);

        return _mapper.MapToPagedResult<SeasonResponse>(seasons);
    }
}