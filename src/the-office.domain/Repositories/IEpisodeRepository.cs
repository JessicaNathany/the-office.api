using the_office.domain.Entities;

namespace the_office.domain.Repositories
{
    public interface IEpisodeRepository : IBaseRepository<Episode>
    {
        Task<Episode?> Get(int id);
        Task Insert(Episode episode);
    }
}