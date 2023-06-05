using Microsoft.Extensions.Logging;
using the_office.domain.Repositories;
using the_office.infrastructure.Data.Context;

namespace the_office.infrastructure.Data;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly TheOfficeDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(TheOfficeDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task BeginTransaction()
    {
        return _context.Database.BeginTransactionAsync();
    }

    public async Task<bool> Commit(CancellationToken cancellationToken)
    {
        if (_context.Database.CurrentTransaction is null)
        {
            _logger.LogWarning("There is no transaction being initiated");
            return false;
        }
        
        try
        {
            await _context.Database.CurrentTransaction.CommitAsync(cancellationToken);
            
            return true;
        }
        catch (Exception exception)
        {
            await Rollback(cancellationToken);
            _logger.LogError(exception, "Error when trying commit a transaction");
            return false;
        }
    }

    public Task Rollback(CancellationToken cancellationToken)
    {
        if (_context.Database.CurrentTransaction is null)
        {
            _logger.LogWarning("There is no transaction being initiated");
            return Task.CompletedTask;
        }
        
        return _context.Database.CurrentTransaction.RollbackAsync(cancellationToken);
    }

    private bool _disposed = false;

    ~UnitOfWork() => Dispose();

    public void Dispose()
    {
        if (!_disposed)
            _context.Dispose();
        
        GC.SuppressFinalize(this);
    }
}