﻿using the_office.api.application.Characters.Requests;
using the_office.api.application.Characters.Responses;
using the_office.domain.Interface;
using the_office.insfrastructure.Mediator.Message;

namespace the_office.api.application.Characters.Handlers
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