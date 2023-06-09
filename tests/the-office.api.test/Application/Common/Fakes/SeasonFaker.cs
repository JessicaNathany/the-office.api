using Bogus;
using the_office.domain.Entities;

namespace the_office.api.test.Application.Common.Fakes;

internal sealed class SeasonFaker : Faker<Season>
{
    private SeasonFaker()
    {
        CustomInstantiator(faker => new Season(
            faker.Name.FullName()
        ));
    }
    
    
    internal static SeasonFaker Create() => new();

    internal Season? WithNull() => null;
}