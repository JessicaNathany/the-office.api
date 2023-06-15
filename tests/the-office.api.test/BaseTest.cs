using AutoFixture;
using AutoFixture.AutoMoq;
using the_office.api.application.Common.Mappings;

namespace the_office.api.test;

public class BaseTest
{
    protected Fixture Fixture { get; }
    protected IMapper Mapper { get; private set;  }

    protected BaseTest()
    {
        Fixture = new Fixture();
        Fixture.Customize(new AutoMoqCustomization());
        
        IConfigurationProvider configuration = new MapperConfiguration(config => config.AddProfile<MappingProfile>());
        Mapper = configuration.CreateMapper();
    }
}