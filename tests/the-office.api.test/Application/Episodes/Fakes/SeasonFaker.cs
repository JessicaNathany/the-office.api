using Bogus;
using the_office.domain.Entities;

namespace the_office.api.test.Application.Episodes.Fakes;

internal sealed class SeasonFaker : Faker<Season>
{
    private SeasonFaker()
    {
        CustomInstantiator(faker => new Season(
            faker.Name.FullName()
        ));
    }
    
    internal static Season Create()
    {
        var faker = new SeasonFaker();
        return faker.Generate();
    }
}