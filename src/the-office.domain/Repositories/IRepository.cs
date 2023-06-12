using System.Linq.Expressions;
using the_office.domain.Shared;

namespace the_office.domain.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task<TEntity?> GetById(int id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByCode(Guid code, CancellationToken cancellationToken = default);
    Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<List<TEntity>?> GetAll(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<IPagedResult<TEntity>> GetAll(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<IPagedResult<TEntity>> GetAll(Expression<Func<TEntity, bool>> filterExpression, int page, int pageSize, CancellationToken cancellationToken = default);
    IQueryable<TEntity> GetQueryable();
    Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> Any(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}