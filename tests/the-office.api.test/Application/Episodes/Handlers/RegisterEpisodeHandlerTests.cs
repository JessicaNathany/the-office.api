using the_office.api.application.Common.Mappings;
using the_office.api.application.Episodes.Handlers;
using the_office.api.test.Application.Common.Fakes;
using the_office.api.test.Application.Episodes.Fakes;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Episodes.Handlers;

[Collection("the-office")]
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
        var request = RegisterEpisodeRequestFaker
            .Create()
            .Generate();
        
        var fakeSeason = SeasonFaker
            .Create()
            .Generate();
        
        var fakerCharacters = CharacterFaker
            .Create()
            .WithMany();

        _seasonRepository.Setup(repository => repository.Get(season => season.Code == request.SeasonCode, CancellationToken.None))
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
        response.Value.Characters.Should().HaveSameCount(fakerCharacters);
        
        _episodeRepository.Verify(r => r.Add(It.IsAny<Episode>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task RegisterEpisode_WhenInvalidSeasonCode_ShouldFailWithSeasonNotFound()
    {
        // Arrange
        var request = RegisterEpisodeRequestFaker
            .Create()
            .Generate();
        
        var fakeSeason = SeasonFaker
            .Create()
            .WithNull();

        _seasonRepository.Setup(repository => repository.Get(season => season.Code == request.SeasonCode, CancellationToken.None))
            .ReturnsAsync(fakeSeason);
        
        // Act
        var response = await _registerEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Code.Should().Be(EpisodeError.SeasonNotValid.Code);
        response.Error.Message.Should().Be(EpisodeError.SeasonNotValid.Message);
        
        _episodeRepository.Verify(r => r.Add(It.IsAny<Episode>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
    
    [Fact]
    public async Task RegisterEpisode_WhenCharactersAreInvalid_ShouldFailWithCharactersNotValid()
    {
        // Arrange
        var request = RegisterEpisodeRequestFaker
            .Create()
            .WithManyCharacters(3);
        
        var fakeSeason = SeasonFaker
            .Create()
            .Generate();
        
        var fakerCharacters = CharacterFaker
            .Create()
            .WithMany(5);

        _seasonRepository.Setup(repository => repository.Get(season => season.Code == request.SeasonCode, CancellationToken.None))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.Characters!.Contains(c.Code), default))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _registerEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Code.Should().Be(EpisodeError.CharactersNotValid.Code);
        response.Error.Message.Should().Be(EpisodeError.CharactersNotValid.Message);
        
        _episodeRepository.Verify(r => r.Add(It.IsAny<Episode>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
    
    [Fact]
    public async Task RegisterEpisode_WhenHasNoCharacters_ShouldNotAddCharacters()
    {
        // Arrange
        var request = RegisterEpisodeRequestFaker
            .Create()
            .WithNoCharacters();
        
        var fakeSeason = SeasonFaker
            .Create()
            .Generate();
        
        var fakerCharacters = CharacterFaker
            .Create()
            .WithMany();

        _seasonRepository.Setup(repository => repository.Get(season => season.Code == request.SeasonCode, CancellationToken.None))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.Characters!.Contains(c.Code), CancellationToken.None))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _registerEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
        response.Value.Name.Should().Be(request.Name);
        response.Value.Description.Should().Be(request.Description);
        response.Value.AirDate.Should().Be(request.AirDate);
        
        _characterRepository.Verify(r => r.GetAll(c => request.Characters!.Contains(c.Code), CancellationToken.None), Times.Never);
        _episodeRepository.Verify(r => r.Add(It.IsAny<Episode>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}