using the_office.api.application.Common.Mappings;
using the_office.api.application.Episodes.Handlers;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.test.Application.Common.Fakes;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Episodes.Handlers;

public class RegisterEpisodeHandlerTests
{
    private readonly Mock<IEpisodeRepository> _episodeRepository = new();
    private readonly Mock<ISeasonRepository> _seasonRepository = new();
    private readonly Mock<ICharacterRepository> _characterRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly RegisterEpisodeHandler _registerEpisodeHandler; 

    public RegisterEpisodeHandlerTests()
    {
        IConfigurationProvider configuration = new MapperConfiguration(config => config.AddProfile<MappingProfile>());
        var mapper = configuration.CreateMapper();

        _registerEpisodeHandler = new RegisterEpisodeHandler(_episodeRepository.Object, _seasonRepository.Object,
            _characterRepository.Object, _unitOfWork.Object, mapper);
    }
    
    [Fact]
    public async Task RegisterEpisode_WhenRequestIsValid_ShouldReturnNewEpisode()
    {
        // Arrange
        var request = new RegisterEpisodeRequest(); // TODO: Create a request using Faker
        var fakeSeason = SeasonFaker.Create();
        var fakerCharacters = CharacterFaker.CreateMany();

        _seasonRepository.Setup(repository => repository.GetByCode(request.SeasonCode, default))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.Characters!.Contains(c.Code), default))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _registerEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
        response.Value.Name.Should().Be(request.Name);
        response.Value.Description.Should().Be(request.Description);
        response.Value.AirDate.Should().Be(request.AirDate);
    }
}