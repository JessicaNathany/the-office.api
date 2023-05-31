using Microsoft.EntityFrameworkCore;
using the_office.domain.Repositories;
using the_office.domain.Shared;
using the_office.infrastructure.Data.Context;

namespace the_office.infrastructure.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly TheOfficeDbContext _context;
        protected readonly DbSet<TEntity> _DbSet;

        public BaseRepository(TheOfficeDbContext dbContext)
        {
            _context = dbContext;
            _DbSet = _context.Set<TEntity>();
        }
        public async Task<TEntity> Create(TEntity entity)
        {
            _DbSet.Add(entity);
            await SaveChanges();

            return entity;
        }

        public Task<TEntity> Update(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _DbSet.ToArrayAsync();
        }

        public async Task<TEntity> GetByCode(Guid code)
        {
            return await _DbSet.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
