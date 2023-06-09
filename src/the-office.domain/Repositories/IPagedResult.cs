namespace the_office.domain.Repositories;

public interface IPagedResult<out TEntity> : IEnumerable<TEntity>
{
    int Page { get; }
    int PageSize { get; }
    int PageCount { get; }
    int TotalCount { get; }
}