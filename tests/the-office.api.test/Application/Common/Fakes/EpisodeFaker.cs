using Bogus;
using MockQueryable.Moq;
using the_office.domain.Entities;
using the_office.domain.Repositories;
using the_office.infrastructure.Data.Pagination;

namespace the_office.api.test.Application.Common.Fakes;

internal sealed class EpisodeFaker : Faker<Episode>
{
    private EpisodeFaker()
    {
        CustomInstantiator(faker => new Episode(
            faker.Name.FullName(),
            faker.Name.JobDescriptor(),
            faker.Date.Past(),
            SeasonFaker.Create().Generate()
        ));
        
        RuleFor(episode => episode.Id, faker => faker.Database.Random.Int());
        RuleFor(episode => episode.Code, faker => faker.Database.Random.Guid());

        FinishWith((_, episode) =>
        {
            episode.AddCharacters(CharacterFaker.Create().WithMany());
        });
    }

    internal static EpisodeFaker Create() => new();

    internal IEnumerable<Episode> WithMany(int count = 10) => Generate(count);
    
    internal Episode? WithNull() => null;
    
    internal IQueryable<Episode> AsQueryable(int count = 10) => Generate(count).BuildMock();
    
    internal IQueryable<Episode> AsQueryableEmpty() => Generate(0).BuildMock();

    internal IPagedResult<Episode> AsPaged(int page = 1, int pageSize = 10, int count = 10)
    {
        var episodes = Generate(count).BuildMock();
        
        return new PagedList<Episode>(episodes, page, pageSize);
    }
}