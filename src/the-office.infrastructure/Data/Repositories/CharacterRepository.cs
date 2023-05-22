using the_office.domain.Entities;
using the_office.domain.Interface;
using the_office.domain.Repositories;

namespace the_office.infrastructure.Data.Repositories
{
    public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
    {
    }
}
