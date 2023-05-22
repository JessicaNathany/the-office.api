using the_office.api.application.Characters.Requests;
using the_office.api.application.Characters.Responses;
using the_office.domain.Repositories;
using the_office.insfrastructure.Mediator.Message;

namespace the_office.api.application.Characters.Handlers
{
    public class CreateCharacterHandler : CommandHandler<CreateCharacterRequest, List<CreateCharacterResponse>>
    {
        private readonly ICharacterRepository _characterRepository;

        public CreateCharacterHandler(ICharacterRepository characterRepository)
        {
            _characterRepository= characterRepository;  
        }
        public override Task<CommandResponse<List<CreateCharacterResponse>>> Handle(CreateCharacterRequest request, CancellationToken cancellationToken)
        {
            

            throw new NotImplementedException();
        }
    }
}
