using the_office.domain.Enums;

namespace the_office.domain.Errors;

public static class SeasonError
{
    public static readonly Error NotFound = new(ErrorType.ResourceNotFound, "Episode not found");
    public static readonly Error AlreadyExists = new(ErrorType.ValidationError, "This season has already been registered");
}