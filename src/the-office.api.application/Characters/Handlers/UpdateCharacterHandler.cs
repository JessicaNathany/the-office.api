using AutoMapper;
using MediatR;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Characters.Handlers
{
    public class UpdateCharacterHandler : ICommandHandler<UpdateCharacterRequest, CharacterResponse>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCharacterHandler(ICharacterRepository characterRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<Result<CharacterResponse>> Handle(UpdateCharacterRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            // to be continued
        }
    }
}
