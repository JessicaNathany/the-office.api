using the_office.api.application.Request;
using the_office.api.application.Response;
using the_office.insfrastructure.Mediator.Message;
using the_office.insfrastructure.Repository.Interface;

namespace the_office.api.application.Handlers.Characters
{
    public class CharacterHandler : CommandHandler<CharacterRequest, List<CharacterResponse>>
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterHandler(ICharacterRepository characterRepository)
        {
            _characterRepository= characterRepository;  
        }
        public override Task<CommandResponse<List<CharacterResponse>>> Handle(CharacterRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
