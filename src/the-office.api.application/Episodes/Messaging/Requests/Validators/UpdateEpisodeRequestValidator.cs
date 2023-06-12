using FluentValidation;

namespace the_office.api.application.Episodes.Messaging.Requests.Validators;

internal class UpdateEpisodeRequestValidator : AbstractValidator<UpdateEpisodeRequest>
{
    public UpdateEpisodeRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Description).NotEmpty();
        RuleFor(request => request.AirDate).NotNull();
        RuleFor(request => request.SeasonCode).NotEmpty();
    }
}