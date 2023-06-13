using the_office.api.application.Common.Mappings;
using the_office.api.application.Episodes.Handlers;
using the_office.api.test.Application.Common.Fakes;
using the_office.api.test.Application.Episodes.Fakes;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Episodes.Handlers;

public class UpdateEpisodeHandlerTests
{
    private readonly Mock<IEpisodeRepository> _episodeRepository = new();
    private readonly Mock<ISeasonRepository> _seasonRepository = new();
    private readonly Mock<ICharacterRepository> _characterRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly UpdateEpisodeHandler _updateEpisodeHandler;

    public UpdateEpisodeHandlerTests()
    {
        IConfigurationProvider configuration = new MapperConfiguration(configure => configure.AddProfile<MappingProfile>());
        var mapper = configuration.CreateMapper();

        _updateEpisodeHandler = new UpdateEpisodeHandler(_episodeRepository.Object, _seasonRepository.Object,
            _characterRepository.Object, _unitOfWork.Object, mapper);
    }

    [Fact]
    public async Task UpdateEpisode_WhenRequestIsValid_ShouldReturnUpdatedEpisode()
    {
        // Arrange
        var request = UpdateEpisodeRequestFaker
            .Create()
            .Generate();
        
        var fakeSeason = SeasonFaker
            .Create()
            .Generate();
        
        var fakerCharacters = CharacterFaker
            .Create()
            .WithMany();

        var episode = EpisodeFaker
            .Create().
            Generate();
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, CancellationToken.None))
            .ReturnsAsync(episode);
        
        _seasonRepository.Setup(repository => repository.Get(season => season.Code == request.SeasonCode, CancellationToken.None))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.Characters!.Contains(c.Code), CancellationToken.None))
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
        _unitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task UpdateEpisode_WhenEpisodeIsNotFound_ShouldFailWithEpisodeNotFound()
    {
        // Arrange
        var request = UpdateEpisodeRequestFaker.Create().Generate();
        var fakeSeason = SeasonFaker.Create().Generate();
        var fakerCharacters = CharacterFaker.Create().WithMany();
        var episode = EpisodeFaker.Create().WithNull();
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, CancellationToken.None))
            .ReturnsAsync(episode);
        
        _seasonRepository.Setup(repository => repository.Get(season => season.Code == request.SeasonCode, CancellationToken.None))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.Characters!.Contains(c.Code), CancellationToken.None))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _updateEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Code.Should().Be(EpisodeError.NotFound.Code);
        response.Error.Message.Should().Be(EpisodeError.NotFound.Message);
        
        _episodeRepository.Verify(r => r.Update(It.IsAny<Episode>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
    
    [Fact]
    public async Task UpdateEpisode_WhenInvalidSeasonCode_ShouldFailWithSeasonNotFound()
    {
        // Arrange
        var request = UpdateEpisodeRequestFaker
            .Create()
            .Generate();
        
        var fakeSeason = SeasonFaker
            .Create()
            .WithNull();
        
        var fakerCharacters = CharacterFaker
            .Create()
            .WithMany();

        var episode = EpisodeFaker
            .Create().
            Generate();
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, CancellationToken.None))
            .ReturnsAsync(episode);
        
        _seasonRepository.Setup(repository => repository.Get(season => season.Code == request.SeasonCode, CancellationToken.None))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.Characters!.Contains(c.Code), CancellationToken.None))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _updateEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Code.Should().Be(EpisodeError.SeasonNotValid.Code);
        response.Error.Message.Should().Be(EpisodeError.SeasonNotValid.Message);
        
        _episodeRepository.Verify(r => r.Update(It.IsAny<Episode>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
    
    [Fact]
    public async Task UpdateEpisode_WhenCharactersAreInvalid_ShouldFailWithCharactersNotValid()
    {
        // Arrange
        var request = UpdateEpisodeRequestFaker
            .Create()
            .WithManyCharacters(3);
        
        var fakeSeason = SeasonFaker
            .Create()
            .Generate();
        
        var fakerCharacters = CharacterFaker
            .Create()
            .WithMany(5);

        var episode = EpisodeFaker
            .Create().
            Generate();
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, CancellationToken.None))
            .ReturnsAsync(episode);
        
        _seasonRepository.Setup(repository => repository.Get(season => season.Code == request.SeasonCode, CancellationToken.None))
            .ReturnsAsync(fakeSeason);
        
        _characterRepository.Setup(repository => repository.GetAll(c =>request.Characters!.Contains(c.Code), CancellationToken.None))
            .ReturnsAsync(fakerCharacters);
        
        // Act
        var response = await _updateEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Code.Should().Be(EpisodeError.CharactersNotValid.Code);
        response.Error.Message.Should().Be(EpisodeError.CharactersNotValid.Message);
        
        _episodeRepository.Verify(r => r.Update(It.IsAny<Episode>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
    
    [Fact]
    public async Task UpdateEpisode_WhenHasNoCharacters_ShouldReturnEpisodeWithoutCharacters()
    {
        // Arrange
        var request = UpdateEpisodeRequestFaker
            .Create()
            .WithNoCharacters();
        
        var fakeSeason = SeasonFaker
            .Create()
            .Generate();
        
        var episode = EpisodeFaker
            .Create().
            Generate();
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, CancellationToken.None))
            .ReturnsAsync(episode);
        
        _seasonRepository.Setup(repository => repository.Get(season => season.Code == request.SeasonCode, CancellationToken.None))
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
        
        _characterRepository.Verify(r => r.GetAll(c => request.Characters!.Contains(c.Code), CancellationToken.None), Times.Never);
        _episodeRepository.Verify(r => r.Update(It.IsAny<Episode>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}