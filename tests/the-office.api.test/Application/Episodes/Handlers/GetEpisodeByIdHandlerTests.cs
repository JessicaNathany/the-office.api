using Microsoft.EntityFrameworkCore;
using the_office.api.application.Common.Mappings;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.domain.Repositories;
using the_office.api.application.Episodes.Handlers;
using the_office.api.test.Application.Common.Fakes;
using the_office.domain.Errors;

namespace the_office.api.test.Application.Episodes.Handlers;

public class GetEpisodeByIdHandlerTests
{
    private readonly Mock<IEpisodeRepository> _episodeRepository = new();
    private readonly GetEpisodeByIdHandler _getEpisodeByIdHandler;

    public GetEpisodeByIdHandlerTests()
    {
        IConfigurationProvider configuration = new MapperConfiguration(config => config.AddProfile<MappingProfile>());
        var mapper = configuration.CreateMapper();
        _getEpisodeByIdHandler = new GetEpisodeByIdHandler(_episodeRepository.Object, mapper);
    }
    
    [Fact]
    public async Task GetEpisodeById_WhenEpisodeExists_ShouldReturnEpisode()
    {
        // Arrange
        var fakeEpisodes = EpisodeFaker.CreateQueryable();

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
    }
    
    [Fact]
    public async Task GetEpisodeById_WhenEpisodeNotExists_ShouldReturnNotFound()
    {
        // Arrange
        const int episodeId = 10;
        var request = new GetEpisodeByIdRequest(episodeId);

        var fakeEpisodes = EpisodeFaker.CreateEmptyQueryable();
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