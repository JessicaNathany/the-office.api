using the_office.api.application.Common.Commands;
using the_office.api.application.Common.Pagination;
using the_office.api.application.Seasons.Messaging.Responses;

namespace the_office.api.application.Seasons.Messaging.Requests;

public sealed record GetSeasonsRequest : ICommand<PagedResult<SeasonResponse>>
{
    public GetSeasonsRequest(int? page, int? pageSize)
    {
        Page = page ?? 1;
        PageSize = pageSize ?? 10;
    }

    public int Page { get; init; }
    public int PageSize { get; init; }
}