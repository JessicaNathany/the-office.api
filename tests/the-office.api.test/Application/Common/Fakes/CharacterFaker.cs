using Bogus;
using the_office.domain.Entities;

namespace the_office.api.test.Application.Common.Fakes;

internal sealed class CharacterFaker : Faker<Character>
{
    private CharacterFaker()
    {
        CustomInstantiator(faker => new Character(
            faker.Name.FullName(),
            faker.Name.FullName(),
            faker.Random.Bool(),
            faker.Person.Gender.ToString(),
            faker.Image.PlaceImgUrl(),
            faker.Person.Company.Bs
            ));
    }
    
    internal static Character Create()
    {
        var faker = new CharacterFaker();
        return faker.Generate();
    }
    
    internal static IEnumerable<Character> CreateMany()
    {
        var faker = new CharacterFaker();
        return faker.Generate(10);
    }
}