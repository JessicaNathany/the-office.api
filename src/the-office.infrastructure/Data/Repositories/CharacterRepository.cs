using Microsoft.EntityFrameworkCore;
using the_office.domain.Entities;
using the_office.domain.Repositories;
using the_office.infrastructure.Data.Context;

namespace the_office.infrastructure.Data.Repositories;

public class CharacterRepository : Repository<Character>, ICharacterRepository
{
    private readonly TheOfficeDbContext _context;

    public CharacterRepository(TheOfficeDbContext context) : base(context)
    {
        _context = context;
    }
}