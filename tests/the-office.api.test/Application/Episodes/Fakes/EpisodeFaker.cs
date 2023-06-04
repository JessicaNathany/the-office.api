using Bogus;
using MockQueryable.Moq;
using the_office.domain.Entities;

namespace the_office.api.test.Application.Episodes.Fakes;

internal sealed class EpisodeFaker : Faker<Episode>
{
    private EpisodeFaker()
    {
        CustomInstantiator(faker => new Episode(
            faker.Name.FullName(),
            faker.Name.JobDescriptor(),
            faker.Date.Past(),
            SeasonFaker.Create()
        ));
        
        RuleFor(episode => episode.Id, faker => faker.Database.Random.Int());
    }

    internal static Episode Create()
    {
        var faker = new EpisodeFaker();
        return faker.Generate();
    }
    
    internal static IEnumerable<Episode> CreateMany()
    {
        var faker = new EpisodeFaker();
        return faker.Generate(100);
    }
    
    internal static IQueryable<Episode> CreateQueryable()
    {
        var faker = new EpisodeFaker();
        var episodes = faker.Generate(100);

        return episodes.BuildMock();
    }
    
    internal static IQueryable<Episode> CreateEmptyQueryable()
    {
        var faker = new EpisodeFaker();
        var episodes = faker.Generate(0);

        return episodes.BuildMock();
    }
}