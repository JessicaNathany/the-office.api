using AutoMapper;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
using the_office.domain.Errors;
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
        public async Task<Result<CharacterResponse>> Handle(UpdateCharacterRequest request, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetById(request.Id, cancellationToken);

            if (character is null)
                return Result.Failure<CharacterResponse>(CharacterError.NotFound);

            character.ChangeInfo(request.Name, request.NameActor, request.Status, request.Gender, request.ImageUrl, request.Job);

            try
            {
                _characterRepository.Update(character);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

            }
            catch (Exception)
            {
                await _unitOfWork.Rollback(cancellationToken);
                throw;
            }

            return _mapper.Map<CharacterResponse>(request);
        }
    }
}
