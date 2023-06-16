using Bogus;
using MockQueryable.Moq;
using the_office.domain.Entities;
using the_office.domain.Repositories;
using the_office.infrastructure.Data.Pagination;

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
    
    internal static CharacterFaker Create() => new();

    internal List<Character> WithMany(int count = 10) => Generate(count);

    internal Character? WithNull() => null;

    internal IQueryable<Character> AsQueryable(int count = 10) => Generate(count).BuildMock();

    internal IQueryable<Character> AsQueryableEmpty() => Generate(0).BuildMock();

    internal IPagedResult<Character> AsPaged(int page = 1, int pageSize = 10, int count = 10)
    {
        var episodes = Generate(count).BuildMock();

        return new PagedList<Character>(episodes, page, pageSize);
    }
}