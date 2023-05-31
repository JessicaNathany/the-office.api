using the_office.domain.Entities;

namespace the_office.domain.Repositories
{
    public interface ICharacterRepository : IBaseRepository<Character>
    {
        Task<Character> GetByName(string personaName);
    }
}
