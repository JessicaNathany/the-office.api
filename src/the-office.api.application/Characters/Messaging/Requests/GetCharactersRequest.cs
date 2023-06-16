using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
using the_office.api.application.Common.Pagination;

namespace the_office.api.application.Characters.Messaging.Requests
{
    public sealed record GetCharactersRequest : ICommand<PagedResult<CharacterResponse>>
    {
        public GetCharactersRequest(int? page, int? pageSize)
        {
            Page = page ?? 1;
            PageSize = pageSize ?? 10;
        }

        public int Page { get; }
        public int PageSize { get; }
    }
}
