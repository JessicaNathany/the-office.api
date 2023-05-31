﻿using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Commands;
using the_office.domain.Entities;

namespace the_office.api.application.Characters.Messaging.Requests;
public sealed record InsertCharacterRequest() : ICommand<List<CharacterResponse>>
{
    public string Name { get; set; }

    public string NameActor { get; set; }

    public bool Status { get; set; }

    public string Gender { get; set; }

    public string ImageUrl { get; set; }

    public string Job { get; set; }

    public IEnumerable<EpisodeCharacter> Episodes { get; set; }
}