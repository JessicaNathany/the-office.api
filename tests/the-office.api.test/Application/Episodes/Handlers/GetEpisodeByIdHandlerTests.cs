using AutoFixture;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.domain.Repositories;
using the_office.api.application.Episodes.Handlers;
using the_office.domain.Errors;
using the_office.domain.Entities;

namespace the_office.api.test.Application.Episodes.Handlers;

[Collection("the-office")]
public class GetEpisodeByIdHandlerTests : BaseTest
{
    private readonly Mock<IEpisodeRepository> _episodeRepository = new();
    private readonly GetEpisodeByIdHandler _getEpisodeByIdHandler;

    public GetEpisodeByIdHandlerTests()
    {
        _getEpisodeByIdHandler = new GetEpisodeByIdHandler(_episodeRepository.Object, Mapper);
    }
    
    [Fact]
    public async Task GetEpisodeById_WhenEpisodeExists_ShouldReturnEpisode()
    {
        // Arrange
        var fakeEpisodes = Fixture.CreateMany<Episode>().BuildMock();
        var episode = await fakeEpisodes.FirstOrDefaultAsync();
        var request = new GetEpisodeByIdRequest(episode!.Id);

        _episodeRepository.Setup(repository => repository.GetQueryable())
            .Returns(fakeEpisodes);
        
        // Act
        var response = await _getEpisodeByIdHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
        response.Value.Name.Should().Be(episode.Name);
        response.Value.Description.Should().Be(episode.Description);
        response.Value.AirDate.Should().Be(episode.AirDate);
        response.Value.Characters.Should().HaveSameCount(episode.Characters);
    }
    
    [Fact]
    public async Task GetEpisodeById_WhenEpisodeNotExists_ShouldReturnNotFound()
    {
        // Arrange
        const int episodeId = 10;
        var request = new GetEpisodeByIdRequest(episodeId);
        var fakeEpisodes = Fixture.CreateMany<Episode>(0).BuildMock();
        
        _episodeRepository.Setup(repository => repository.GetQueryable())
            .Returns(fakeEpisodes);
        
        // Act
        var response = await _getEpisodeByIdHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.IsFailure.Should().BeTrue();
        response.Error.Should().Be(EpisodeError.NotFound);
    }
}