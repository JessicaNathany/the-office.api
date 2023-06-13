using AutoMapper;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
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
        public Task<Result<CharacterResponse>> Handle(GetCharacterByIdRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
