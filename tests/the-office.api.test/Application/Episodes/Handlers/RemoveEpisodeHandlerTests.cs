using AutoFixture;
using the_office.api.application.Episodes.Handlers;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Episodes.Handlers;

[Collection("the-office")]
public class RemoveEpisodeHandlerTests : BaseTest
{
    private readonly Mock<IEpisodeRepository> _episodeRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly RemoveEpisodeHandler _removeEpisodeHandler;

    public RemoveEpisodeHandlerTests()
    {
        _removeEpisodeHandler = new RemoveEpisodeHandler(_episodeRepository.Object, _unitOfWork.Object);
    }
    
    [Fact]
    public async Task RemoveEpisode_WhenEpisodeIsFound_ShouldRemoveEpisode()
    {
        // Arrange
        var episode = Fixture.Create<Episode>();
        var request = new RemoveEpisodeRequest(episode.Id);
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, CancellationToken.None))
            .ReturnsAsync(episode);
        
        // Act
        var response = await _removeEpisodeHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
        
        _episodeRepository.Verify(r => r.Remove(It.IsAny<Episode>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task RemoveEpisode_WhenEpisodeNotFound_ShouldFailWithEpisodeNotFound()
    {
        // Arrange
        Episode? episode = null;
        var request = new RemoveEpisodeRequest(1);
        
        _episodeRepository.Setup(repository => repository.GetById(request.Id, CancellationToken.None))
            .ReturnsAsync(episode);
        
        // Act
        var response = await _removeEpisodeHandler.Handle(request, CancellationToken.None);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Code.Should().Be(EpisodeError.NotFound.Code);
        response.Error.Message.Should().Be(EpisodeError.NotFound.Message);
        
        _episodeRepository.Verify(r => r.Remove(It.IsAny<Episode>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
}