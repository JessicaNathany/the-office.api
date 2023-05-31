using AutoMapper;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Characters.Handlers;

public sealed class RegisterCharacterHandler : ICommandHandler<RegisterCharacterRequest, CharacterResponse>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;

    public RegisterCharacterHandler(ICharacterRepository characterRepository, IMapper mapper)
    {
        _characterRepository= characterRepository;  
        _mapper = mapper;
    }

    public async Task<Result<CharacterResponse>> Handle(RegisterCharacterRequest request, CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetByName(request.Name, request.NameActor);

        if (character == null)
            return Result.Failure<CharacterResponse>(CharacterError.Exists);

        var response = _mapper.Map<CharacterResponse>(character);

        return response;
    }
}