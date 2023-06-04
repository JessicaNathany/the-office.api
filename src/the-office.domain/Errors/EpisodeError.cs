using the_office.domain.Enums;

namespace the_office.domain.Errors;

public static class EpisodeError
{
    public static readonly Error NotFound = new(ErrorType.ResourceNotFound, "Episode not found");
    public static readonly Error SeasonNotValid = new(ErrorType.ValidationError, "Season not found");
    public static readonly Error CharactersNotValid = new(ErrorType.ValidationError, "Some characters is not valid");
}