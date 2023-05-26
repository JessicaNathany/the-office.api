using the_office.api.application.Characters.Responses;
using the_office.api.application.Common.Commands;
using the_office.domain.Entities;

namespace the_office.api.application.Characters.Requests;

public sealed record CreateCharacterRequest : ICommand<List<CreateCharacterResponse>>
{
    public string Name { get; set; }

    public string NameActor { get; set; }

    public bool Status { get; set; }

    public string Gender { get; set; }

    public string ImageUrl { get; set; }

    public string Job { get; set; }

    public IEnumerable<EpisodeCharacter> Episodes { get; set; }

    // public bool IsValid()
    // {
    //     var validations = new InlineValidator<CreateCharacterRequest>();
    //
    //     validations.RuleFor(c=> c.Name)
    //         .NotEmpty()
    //         .WithErrorCode("") // to define
    //         .WithMessage(""); // to define
    //
    //     validations.RuleFor(c => c.NameActor)
    //         .NotEmpty()
    //         .WithErrorCode("") // to define
    //         .WithMessage(""); // to define
    //
    //     ValidationResult = validations.Validate(this);
    //
    //     return ValidationResult.IsValid;
    // }
}