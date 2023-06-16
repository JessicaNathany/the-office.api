using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Characters.Handlers
{
    public class GetCharacterByIdHandler : ICommandHandler<GetCharacterByIdRequest, CharacterResponse>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public GetCharacterByIdHandler(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }
        public async Task<Result<CharacterResponse>> Handle(GetCharacterByIdRequest request, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetQueryable()
                .AsNoTracking()
                .ProjectTo<CharacterResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(character => character.Id == request.id, cancellationToken: cancellationToken);

            return character ?? Result.Failure<CharacterResponse>(CharacterError.NotFound);
        }
    }
}
