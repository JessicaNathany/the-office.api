﻿using the_office.api.application.Characters.Messaging.Response;
using the_office.api.application.Common.Mappings;
using the_office.api.application.Seasons.Messaging.Responses;
using the_office.domain.Entities;

namespace the_office.api.application.Episodes.Messaging.Responses;

public record EpisodeResponse : IMapFrom<Episode>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime AirDate { get; set; }
    public string Description { get; set; }
    public SeasonResponse Season { get; set; }
    public IEnumerable<CharacterResponse>? Characters { get; set; }
}