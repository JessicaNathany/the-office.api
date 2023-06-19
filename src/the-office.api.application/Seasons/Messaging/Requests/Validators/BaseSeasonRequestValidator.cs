using FluentValidation;

namespace the_office.api.application.Seasons.Messaging.Requests.Validators;

internal class BaseSeasonRequestValidator : AbstractValidator<BaseSeasonRequest>
{
    public BaseSeasonRequestValidator()
    {
        RuleFor(request => request.Title).NotEmpty();
        RuleFor(request => request.Number).GreaterThan(0);
        RuleFor(request => request.ReleaseDate).NotNull();
        RuleFor(request => request.TotalEpisodes).GreaterThan(0);
        RuleFor(request => request.Summary).NotEmpty();
    }
}