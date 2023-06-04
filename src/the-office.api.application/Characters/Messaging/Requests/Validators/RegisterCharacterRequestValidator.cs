using FluentValidation;

namespace the_office.api.application.Characters.Messaging.Requests.Validators
{
    public class RegisterCharacterRequestValidator : AbstractValidator<RegisterCharacterRequest>
    {
        public RegisterCharacterRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty();
            RuleFor(request => request.NameActor).NotEmpty();
            RuleFor(request => request.Job).NotNull();
        }
    }
}
