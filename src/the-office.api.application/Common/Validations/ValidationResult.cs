using the_office.domain.Errors;
using the_office.domain.Shared;

namespace the_office.api.application.Common.Validations;

public class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(false, IValidationResult.ValidationError)
        => Errors = errors;

    public Error[] Errors { get; }

    public IDictionary<string, string[]> ToDictionary()
    {
        return Errors
            .GroupBy(error => error.Code, error => error.Message)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}

public class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    protected internal ValidationResult(Error[] errors)
        : base(default, false, IValidationResult.ValidationError) =>
        Errors = errors;

    public Error[] Errors { get; }

    public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
    
    public IDictionary<string, string[]> ToDictionary()
    {
        return Errors
            .GroupBy(error => error.Code, error => error.Message)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}