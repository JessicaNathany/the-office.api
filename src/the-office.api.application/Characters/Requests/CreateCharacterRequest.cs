using FluentValidation;
using the_office.api.application.Characters.Responses;
using the_office.domain.Entities;
using the_office.domain.Enums;
using the_office.insfrastructure.Mediator.Message;

namespace the_office.api.application.Characters.Requests
{
    public class CreateCharacterRequest : CommandHandler<List<CreateCharacterResponse>>
    {
        public string Name { get; set; }

        public string NameActor { get; set; }

        public bool Status { get; set; }

        public string Gender { get; set; }

        public string ImageUrl { get; set; }

        public string Job { get; set; }

        public IEnumerable<EpisodeCharacter> Episodes { get; set; }

        public override bool IsValid()
        {
            var validations = new InlineValidator<CreateCharacterRequest>();

            validations.RuleFor(c=> c.Name)
                .NotEmpty()
                .WithErrorCode("") // to define
                .WithMessage(""); // to define

            validations.RuleFor(c => c.NameActor)
               .NotEmpty()
               .WithErrorCode("") // to define
               .WithMessage(""); // to define

            ValidationResult = validations.Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
