using the_office.domain.Model;

namespace the_office.domain.Interface
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> GetById(int id);

        Task<TEntity> GetByCode(Guid code);

        Task<IEnumerable<TEntity>> GetAll();

        Task<int> SaveChanges();
    }
}
