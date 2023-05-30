using the_office.domain.Entities;
using the_office.domain.Repositories;
using the_office.infrastructure.Data.Context;

namespace the_office.infrastructure.Data.Repositories
{
    public class EpisodeRepository : BaseRepository<Episode>, IEpisodeRepository
    {
        private readonly TheOfficeDbContext _context;

        public EpisodeRepository(TheOfficeDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Episode?> Get(int id)
        {
            var episode = new Episode
            {
                Id = id,
                Name = "Health Care",
                Description = "Episode description"
            };

            return await Task.FromResult(episode);
        }
    }
}
