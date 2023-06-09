using AutoMapper;
using the_office.api.application.Common.Pagination;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Common.Mappings;

public static class MappingExtensions
{
    public static PagedResult<TResponse> MapToPagedResult<TResponse>(this IMapper mapper, IPagedResult<Entity> pagedResult)
    {
        var items = mapper.Map<IEnumerable<TResponse>>(pagedResult);
        
        return PagedResult<TResponse>.Create(items, pagedResult.Page, pagedResult.PageSize, pagedResult.PageCount, pagedResult.TotalCount);
    }
}