using the_office.domain.Entities;
using the_office.domain.Repositories;
using the_office.infrastructure.Data.Context;

namespace the_office.infrastructure.Data.Repositories;

public class SeasonRepository : Repository<Season>, ISeasonRepository
{
    public SeasonRepository(TheOfficeDbContext context) : base(context)
    {
    }
}