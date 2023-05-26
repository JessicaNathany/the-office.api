using the_office.domain.Enums;
using the_office.domain.Errors;

namespace the_office.api.application.Common.Commands;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(ErrorType.ValidationError, "A validation problem occurred.");

    Error[] Errors { get; }
}