using the_office.domain.Entities;

namespace the_office.domain.Repositories;

public interface ICharacterRepository : IRepository<Character>
{
    Task<Character> GetByName(string personaName, string actorName);
}