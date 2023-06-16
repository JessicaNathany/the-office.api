using AutoMapper;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
using the_office.api.application.Common.Mappings;
using the_office.api.application.Common.Pagination;
using the_office.api.application.Episodes.Messaging.Responses;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Characters.Handlers
{
    public class GetCharactersHandler : ICommandHandler<GetCharactersRequest, PagedResult<CharacterResponse>>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public GetCharactersHandler(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<CharacterResponse>>> Handle(GetCharactersRequest request, CancellationToken cancellationToken)
        {
            var characters = await _characterRepository.GetAll(request.Page, request.PageSize, cancellationToken);

            return _mapper.MapToPagedResult<CharacterResponse>(characters);
        }
    }
}
