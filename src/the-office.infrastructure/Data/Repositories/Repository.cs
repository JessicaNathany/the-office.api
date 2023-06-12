using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using the_office.domain.Repositories;
using the_office.domain.Shared;
using the_office.infrastructure.Data.Context;
using the_office.infrastructure.Data.Pagination;

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
    
    public async Task<List<TEntity>?> GetAll(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }
    
    public async Task<IPagedResult<TEntity>> GetAll(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = GetQuery(x => true);
        
        return await PagedList<TEntity>.CreateAsync(query, page, pageSize, cancellationToken);
    }

    public async Task<IPagedResult<TEntity>> GetAll(Expression<Func<TEntity, bool>> filterExpression, int page, int pageSize, 
        CancellationToken cancellationToken = default)
    {
        var query = GetQuery(filterExpression);
        return await PagedList<TEntity>.CreateAsync(query, page, pageSize, cancellationToken);
    }
        
    public async Task<TEntity?> GetById(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
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

    private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>>? filterExpression)
    {
        var queryable = GetQueryable();
        
        return filterExpression is not null ? queryable.Where(filterExpression) : queryable;
    }
}