using the_office.domain.Enums;

namespace the_office.domain.Errors;

public static class EpisodeError
{
    public static readonly Error NotFound = new(ErrorType.ResourceNotFound, "Episode not found");
}