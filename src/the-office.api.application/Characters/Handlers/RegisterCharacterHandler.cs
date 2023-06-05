using AutoMapper;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Characters.Handlers;

public sealed class RegisterCharacterHandler : ICommandHandler<RegisterCharacterRequest, CharacterResponse>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCharacterHandler(ICharacterRepository characterRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CharacterResponse>> Handle(RegisterCharacterRequest request,
        CancellationToken cancellationToken)
    {
        var characterExist = await _characterRepository.GetByName(request.Name, request.NameActor);

        if (characterExist != null)
            return Result.Failure<CharacterResponse>(CharacterError.Exists);

        // TODO: Need create an instance of character
        var character = new Character();

        await _unitOfWork.BeginTransaction();

        _characterRepository.Add(character);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var success = await _unitOfWork.Commit(cancellationToken);
        if (!success)
            return Result.Failure<CharacterResponse>(CharacterError.ErrorWhenRegister);

        return _mapper.Map<CharacterResponse>(character);
    }
}