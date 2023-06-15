using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using the_office.api.application.Common.Mappings;
using the_office.domain.Entities;

namespace the_office.api.test;

public class BaseTest
{
    protected Fixture Fixture { get; }
    protected IMapper Mapper { get; private set;  }

    protected BaseTest()
    {
        Fixture = new Fixture();
        ConfigureFixture();

        IConfigurationProvider configuration = new MapperConfiguration(config => config.AddProfile<MappingProfile>());
        Mapper = configuration.CreateMapper();
    }

    private void ConfigureFixture()
    {
        Fixture.Customize(new AutoMoqCustomization());
        Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        Fixture.Customizations.Add(new TypeRelay(typeof(Episode), typeof(Episode)));
    }
}