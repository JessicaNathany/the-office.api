using the_office.domain.Shared;

namespace the_office.domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetById(int id);

        Task<TEntity> GetByCode(Guid code);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Create(TEntity entity);

        Task Delete(int id);

        Task<TEntity> Update(int id);

        Task SaveChanges();
    }
}
