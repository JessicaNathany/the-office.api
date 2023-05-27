using the_office.domain.Entities;
using the_office.domain.Repositories;

namespace the_office.infrastructure.Data.Repositories;

public class EpisodeRepository : BaseRepository<Episode>, IEpisodeRepository
{
    public async Task<Episode?> Get(int id)
    {
        if (id == 2) return null;
        
        var episode = new Episode
        {
            Id = id,
            Name = "Health Care",
            Description = "Episode description"
        };

        return await Task.FromResult(episode);
    }

    public async Task Insert(Episode episode)
    {
        episode.Id = 1;

        await Task.CompletedTask;
    }
}