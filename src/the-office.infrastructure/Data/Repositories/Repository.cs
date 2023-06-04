using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using the_office.domain.Repositories;
using the_office.domain.Shared;
using the_office.infrastructure.Data.Context;

namespace the_office.infrastructure.Data.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly TheOfficeDbContext _context;

    protected Repository(TheOfficeDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().FindAsync(new object?[] { predicate }, cancellationToken: cancellationToken);
    }
    
    public async Task<IEnumerable<TEntity>?> GetAll(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }
        
    public async Task<TEntity?> GetById(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }
        
    public async Task<TEntity?> GetByCode(Guid code, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>()
            .FirstOrDefaultAsync(entity => entity.Code == code, cancellationToken: cancellationToken);
    }
        
    public IQueryable<TEntity> GetQueryable()
    {
        return _context.Set<TEntity>();
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Where(predicate).CountAsync(cancellationToken);
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Where(predicate).AnyAsync(cancellationToken);
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }
}