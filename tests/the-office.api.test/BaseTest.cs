using AutoFixture;
using AutoFixture.AutoMoq;
using the_office.api.application.Common.Mappings;
using the_office.domain.Entities;

namespace the_office.api.test;

public class BaseTest
{
    protected Fixture Fixture { get; }
    protected IMapper Mapper { get; }

    protected BaseTest()
    {
        IConfigurationProvider configuration = new MapperConfiguration(config => config.AddProfile<MappingProfile>());
        Mapper = configuration.CreateMapper();
        
        Fixture = new Fixture();
        ConfigureFixture();
    }

    private void ConfigureFixture()
    {
        Fixture.Customize(new AutoMoqCustomization());
        Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        CreateFactories();
    }

    private void CreateFactories()
    {
        Fixture.Customize<Season>(composer =>
        {
            return composer.FromFactory((int number, string title, int totalEpisodes, DateTime releaseDate) =>
                new Season(number, title, totalEpisodes, releaseDate, ""));
        });

        Fixture.Customize<Episode>(composer =>
        {
            return composer.FromFactory((string name, string description, DateTime airDate) =>
                new Episode(name, description, airDate, null!));
        });
    }
}