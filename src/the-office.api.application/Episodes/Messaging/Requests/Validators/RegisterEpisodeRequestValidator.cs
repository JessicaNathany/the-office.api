using FluentValidation;

namespace the_office.api.application.Episodes.Messaging.Requests.Validators;

internal class RegisterEpisodeRequestValidator : AbstractValidator<RegisterEpisodeRequest>
{
    public RegisterEpisodeRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Description).NotEmpty();
        RuleFor(request => request.AirDate).NotNull();
        RuleFor(request => request.SeasonNumber).NotEmpty();
    }
}