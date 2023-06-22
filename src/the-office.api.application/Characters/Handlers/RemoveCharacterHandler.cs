using AutoMapper;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Characters.Handlers
{
    public class RemoveCharacterHandler : ICommandHandler<RemoveCharacterRequest>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveCharacterHandler(ICharacterRepository characterRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveCharacterRequest request, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetById(request.Id, cancellationToken);

            if (character is null)
                return Result.Failure<CharacterResponse>(CharacterError.NotFound);

            try
            {
                _characterRepository.Remove(character);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var success = await _unitOfWork.Commit(cancellationToken);

                if (!success)
                    return Result.Failure<CharacterResponse>(CharacterError.ErrorWhenRegister);
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback(cancellationToken);
                throw;
            }

            return Result.Success();
        }
    }
}
