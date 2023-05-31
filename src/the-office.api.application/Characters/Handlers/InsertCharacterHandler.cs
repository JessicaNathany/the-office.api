using AutoMapper;
using MediatR;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Characters.Handlers;

internal sealed class InsertCharacterHandler : ICommandHandler<InsertCharacterRequest, List<CharacterResponse>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;

    public InsertCharacterHandler(ICharacterRepository characterRepository, IMapper mapper)
    {
        _characterRepository= characterRepository;  
        _mapper = mapper;
    }

    Task<Result<List<CharacterResponse>>> IRequestHandler<InsertCharacterRequest, Result<List<CharacterResponse>>>.Handle(InsertCharacterRequest request, CancellationToken cancellationToken)
    {
        //var character = _characterRepository.GetByName(request.Name);

        throw new NotImplementedException();

    }
}