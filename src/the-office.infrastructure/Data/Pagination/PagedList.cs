using Microsoft.EntityFrameworkCore;
using the_office.domain.Repositories;

namespace the_office.infrastructure.Data.Pagination;

public class PagedList<T> : List<T>, IPagedResult<T>
{
    public PagedList(IQueryable<T> source, int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = source.Count();
        PageCount = GetPageCount(pageSize, TotalCount);
        var skip = (Page - 1) * PageSize;

        AddRange(source.Skip(skip).Take(PageSize).ToList());
    }

    public PagedList(int totalCount, int page, int pageSize, IEnumerable<T> results)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        PageCount = GetPageCount(pageSize, TotalCount);
        AddRange(results);
    }

    public int Page { get; }
    public int PageSize { get; }
    public int PageCount { get; }
    public int TotalCount { get; }

    private static int GetPageCount(int pageSize, int totalCount)
    {
        if (pageSize == 0) return 0;
        
        var remainder = totalCount % pageSize;
        return (totalCount / pageSize) + (remainder == 0 ? 0 : 1);
    }

    public static async Task<IPagedResult<T>> CreateAsync(IQueryable<T> source, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);
        var skip = ((page - 1) * pageSize);

        var results = await source
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return new PagedList<T>(count, page, pageSize, results);
    }
}