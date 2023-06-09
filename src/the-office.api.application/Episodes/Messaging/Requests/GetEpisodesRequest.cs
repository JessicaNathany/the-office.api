using the_office.api.application.Common.Commands;
using the_office.api.application.Common.Pagination;
using the_office.api.application.Episodes.Messaging.Responses;

namespace the_office.api.application.Episodes.Messaging.Requests;

public sealed record GetEpisodesRequest : ICommand<PagedResult<EpisodeResponse>>
{
    public GetEpisodesRequest(int? page, int? pageSize)
    {
        Page = page ?? 1;
        PageSize = pageSize ?? 10;
    }

    public int Page { get; }
    public int PageSize { get; }
}