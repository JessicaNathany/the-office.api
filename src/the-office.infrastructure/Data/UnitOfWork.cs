using the_office.domain.Repositories;
using the_office.infrastructure.Data.Context;

namespace the_office.infrastructure.Data;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly TheOfficeDbContext _context;

    public UnitOfWork(TheOfficeDbContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}