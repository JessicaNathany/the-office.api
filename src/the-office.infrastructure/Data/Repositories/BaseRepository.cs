using the_office.domain.Common;
using the_office.domain.Repositories;

namespace the_office.infrastructure.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, new()
    {
       
    }
}
