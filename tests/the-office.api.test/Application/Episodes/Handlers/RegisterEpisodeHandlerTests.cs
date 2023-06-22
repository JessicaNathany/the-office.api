using AutoFixture;
using the_office.api.application.Episodes.Handlers;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Episodes.Handlers;

[Collection("the-office")]
public class RegisterEpisodeHandlerTests : BaseTest
{
    private readonly Mock<IEpisodeRepository> _episodeRepository = new();
    private readonly Mock<ISeasonRepository> _seasonRepository = new();
    private readonly Mock<ICharacterRepository> _characterRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly RegisterEpisodeHandler _registerEpisodeHandler; 

    public RegisterEpisodeHandlerTests()
    {
        _registerEpisodeHandler = new RegisterEpisodeHandler(_episodeRepository.Object, _seasonRepository.Object,
            _characterRepository.Object, _unitOfWork.Object, Mapper);
    }
    
    [Fact]
    public async Task RegisterEpisode_WhenRequestIsValid_ShouldReturnNewEpisode()
    {
        // Arrange
        var request = Fixture.Create<RegisterEpisodeRequest>();
        var fakeSeason = Fixture.Create<Season>();
        var fakerCharacters = Fixture.CreateMany<Character>().ToList();

        _seasonRepository.Setup(repository => repository.Get(season => season.Number == request.SeasonNumber, default))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.CharacterIds!.Contains(c.Id), default))
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
        _unitOfWork.Verify(u => u.SaveChangesAsync(default), Times.Once);
    }
    
    [Fact]
    public async Task RegisterEpisode_WhenInvalidSeasonCode_ShouldFailWithSeasonNotFound()
    {
        // Arrange
        var request = Fixture.Create<RegisterEpisodeRequest>();
        Season? fakeSeason = null;

        _seasonRepository.Setup(repository => repository.Get(season => season.Number == request.SeasonNumber, default))
            .ReturnsAsync(fakeSeason);
        
        // Act
        var response = await _registerEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Code.Should().Be(EpisodeError.SeasonNotValid.Code);
        response.Error.Message.Should().Be(EpisodeError.SeasonNotValid.Message);
        
        _episodeRepository.Verify(r => r.Add(It.IsAny<Episode>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(default), Times.Never);
    }
    
    [Fact]
    public async Task RegisterEpisode_WhenCharactersAreInvalid_ShouldFailWithCharactersNotValid()
    {
        // Arrange
        var request = Fixture.Build<RegisterEpisodeRequest>()
            .With(r => r.CharacterIds, Fixture.CreateMany<int>(10).ToList)
            .Create();

        var fakeSeason = Fixture.Create<Season>();
        var fakerCharacters = Fixture.CreateMany<Character>(5).ToList();

        _seasonRepository.Setup(repository => repository.Get(season => season.Number == request.SeasonNumber, default))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.CharacterIds!.Contains(c.Id), default))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _registerEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Code.Should().Be(EpisodeError.CharactersNotValid.Code);
        response.Error.Message.Should().Be(EpisodeError.CharactersNotValid.Message);
        
        _episodeRepository.Verify(r => r.Add(It.IsAny<Episode>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(default), Times.Never);
    }
    
    [Fact]
    public async Task RegisterEpisode_WhenHasNoCharacters_ShouldNotAddCharacters()
    {
        // Arrange
        var request = Fixture.Build<RegisterEpisodeRequest>()
            .Without(r => r.CharacterIds)
            .Create();

        var fakeSeason = Fixture.Create<Season>();
        var fakerCharacters = Fixture.CreateMany<Character>().ToList();

        _seasonRepository.Setup(repository => repository.Get(season => season.Number == request.SeasonNumber, default))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.CharacterIds!.Contains(c.Id), default))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _registerEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
        response.Value.Name.Should().Be(request.Name);
        response.Value.Description.Should().Be(request.Description);
        response.Value.AirDate.Should().Be(request.AirDate);
        
        _characterRepository.Verify(r => r.GetAll(c => request.CharacterIds!.Contains(c.Id), default), Times.Never);
        _episodeRepository.Verify(r => r.Add(It.IsAny<Episode>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(default), Times.Once);
    }
}