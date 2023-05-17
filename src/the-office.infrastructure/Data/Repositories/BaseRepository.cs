using the_office.domain.Interface;
using the_office.domain.Model;

namespace the_office.infrastructure.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, new()
    {
        public Task<IEnumerable<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByCode(Guid code)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
            //Db?.Dispose
        }
    }
}
