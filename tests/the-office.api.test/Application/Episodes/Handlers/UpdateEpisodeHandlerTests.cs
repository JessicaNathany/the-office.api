using AutoFixture;
using the_office.api.application.Episodes.Handlers;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Episodes.Handlers;

[Collection("the-office")]
public class UpdateEpisodeHandlerTests : BaseTest
{
    private readonly Mock<IEpisodeRepository> _episodeRepository = new();
    private readonly Mock<ISeasonRepository> _seasonRepository = new();
    private readonly Mock<ICharacterRepository> _characterRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly UpdateEpisodeHandler _updateEpisodeHandler;

    public UpdateEpisodeHandlerTests()
    {
        _updateEpisodeHandler = new UpdateEpisodeHandler(_episodeRepository.Object, _seasonRepository.Object,
            _characterRepository.Object, _unitOfWork.Object, Mapper);
    }

    [Fact]
    public async Task UpdateEpisode_WhenRequestIsValid_ShouldReturnUpdatedEpisode()
    {
        // Arrange
        var request = Fixture.Create<UpdateEpisodeRequest>();
        var fakeSeason = Fixture.Create<Season>();
        var fakerCharacters = Fixture.CreateMany<Character>().ToList();
        var episode = Fixture.Create<Episode>();
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, default))
            .ReturnsAsync(episode);
        
        _seasonRepository.Setup(repository => repository.Get(season => season.Number == request.SeasonNumber, default))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.CharacterIds!.Contains(c.Id), default))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _updateEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
        response.Value.Name.Should().Be(request.Name);
        response.Value.Description.Should().Be(request.Description);
        response.Value.AirDate.Should().Be(request.AirDate);
        response.Value.Characters.Should().HaveSameCount(fakerCharacters);
        
        _episodeRepository.Verify(r => r.Update(It.IsAny<Episode>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(default), Times.Once);
    }
    
    [Fact]
    public async Task UpdateEpisode_WhenEpisodeIsNotFound_ShouldFailWithEpisodeNotFound()
    {
        // Arrange
        var request = Fixture.Create<UpdateEpisodeRequest>();
        var fakeSeason = Fixture.Create<Season>();
        var fakerCharacters = Fixture.CreateMany<Character>().ToList();
        Episode? episode = null;
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, default))
            .ReturnsAsync(episode);
        
        _seasonRepository.Setup(repository => repository.Get(season => season.Number == request.SeasonNumber, default))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.CharacterIds!.Contains(c.Id), default))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _updateEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Code.Should().Be(EpisodeError.NotFound.Code);
        response.Error.Message.Should().Be(EpisodeError.NotFound.Message);
        
        _episodeRepository.Verify(r => r.Update(It.IsAny<Episode>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(default), Times.Never);
    }
    
    [Fact]
    public async Task UpdateEpisode_WhenInvalidSeasonCode_ShouldFailWithSeasonNotFound()
    {
        // Arrange
        var request = Fixture.Create<UpdateEpisodeRequest>();
        var fakerCharacters = Fixture.CreateMany<Character>().ToList();
        var episode = Fixture.Create<Episode>();
        Season? fakeSeason = null;
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, default))
            .ReturnsAsync(episode);
        
        _seasonRepository.Setup(repository => repository.Get(season => season.Number == request.SeasonNumber, default))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.CharacterIds!.Contains(c.Id), default))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _updateEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Code.Should().Be(EpisodeError.SeasonNotValid.Code);
        response.Error.Message.Should().Be(EpisodeError.SeasonNotValid.Message);
        
        _episodeRepository.Verify(r => r.Update(It.IsAny<Episode>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(default), Times.Never);
    }
    
    [Fact]
    public async Task UpdateEpisode_WhenCharactersAreInvalid_ShouldFailWithCharactersNotValid()
    {
        // Arrange
        var request = Fixture.Build<UpdateEpisodeRequest>()
            .With(r => r.CharacterIds, Fixture.CreateMany<int>(10).ToList())
            .Create();
        
        var fakeSeason = Fixture.Create<Season>();
        var fakerCharacters = Fixture.CreateMany<Character>(5).ToList();
        var episode = Fixture.Create<Episode>();
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, default))
            .ReturnsAsync(episode);
        
        _seasonRepository.Setup(repository => repository.Get(season => season.Number == request.SeasonNumber, default))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.CharacterIds!.Contains(c.Id), default))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _updateEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Code.Should().Be(EpisodeError.CharactersNotValid.Code);
        response.Error.Message.Should().Be(EpisodeError.CharactersNotValid.Message);
        
        _episodeRepository.Verify(r => r.Update(It.IsAny<Episode>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(default), Times.Never);
    }
    
    [Fact]
    public async Task UpdateEpisode_WhenHasNoCharacters_ShouldReturnEpisodeWithoutCharacters()
    {
        // Arrange
        var request = Fixture.Build<UpdateEpisodeRequest>()
            .Without(r => r.CharacterIds)
            .Create();

        var fakeSeason = Fixture.Create<Season>();
        var episode = Fixture.Create<Episode>();
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, default))
            .ReturnsAsync(episode);
        
        _seasonRepository.Setup(repository => repository.Get(season => season.Number == request.SeasonNumber, default))
            .ReturnsAsync(fakeSeason);
        
        // Act
        var response = await _updateEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
        response.Value.Name.Should().Be(request.Name);
        response.Value.Description.Should().Be(request.Description);
        response.Value.AirDate.Should().Be(request.AirDate);
        response.Value.Characters.Should().BeEmpty();
        
        _characterRepository.Verify(r => r.GetAll(c => request.CharacterIds!.Contains(c.Id), default), Times.Never);
        _episodeRepository.Verify(r => r.Update(It.IsAny<Episode>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(default), Times.Once);
    }
}