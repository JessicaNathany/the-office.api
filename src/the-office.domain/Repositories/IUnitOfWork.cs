namespace the_office.domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task BeginTransaction();
    
    Task<bool> Commit(CancellationToken cancellationToken);
    
    Task Rollback(CancellationToken cancellationToken);
}