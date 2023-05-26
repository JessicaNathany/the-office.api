using the_office.api.application.Characters.Requests;
using the_office.api.application.Characters.Responses;
using the_office.api.application.Common.Commands;
using the_office.domain.Repositories;
using the_office.domain.Shared;

namespace the_office.api.application.Characters.Handlers;

internal sealed class CreateCharacterHandler : ICommandHandler<CreateCharacterRequest, List<CreateCharacterResponse>>
{
    private readonly ICharacterRepository _characterRepository;

    public CreateCharacterHandler(ICharacterRepository characterRepository)
    {
        _characterRepository= characterRepository;  
    }
    public Task<Result<List<CreateCharacterResponse>>> Handle(CreateCharacterRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}