using AutoMapper;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;
using the_office.infrastructure;

namespace the_office.api.application.Characters.Handlers;

public sealed class RegisterCharacterHandler : ICommandHandler<RegisterCharacterRequest, CharacterResponse>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;
    private readonly ITransactionManager _transactionManager;

    public RegisterCharacterHandler(ICharacterRepository characterRepository, IMapper mapper, ITransactionManager transactionManager)
    {
        _characterRepository= characterRepository;  
        _mapper = mapper;
        _transactionManager = transactionManager;    
    }

    public async Task<Result<CharacterResponse>> Handle(RegisterCharacterRequest request, CancellationToken cancellationToken)
    {
        var characterExist = await _characterRepository.GetByName(request.Name, request.NameActor);

        if (characterExist != null)
            return Result.Failure<CharacterResponse>(CharacterError.Exists);

        try
        {
            await _characterRepository.SaveChanges();
            await _transactionManager.Commit();
        }
        catch (Exception)
        {
            _transactionManager.Rollback();
            throw;
        }

        var response = _mapper.Map<CharacterResponse>(request);

        return response;
    }
}