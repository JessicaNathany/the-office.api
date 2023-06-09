using the_office.api.application.Common.Mappings;
using the_office.api.application.Episodes.Handlers;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.test.Application.Common.Fakes;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Episodes.Handlers;

public class GetEpisodesHandlerTests
{
    private readonly Mock<IEpisodeRepository> _episodeRepository = new();
    private readonly GetEpisodesHandler _getEpisodesHandler;

    public GetEpisodesHandlerTests()
    {
        IConfigurationProvider configuration = new MapperConfiguration(config => config.AddProfile<MappingProfile>());
        var mapper = configuration.CreateMapper();

        _getEpisodesHandler = new GetEpisodesHandler(_episodeRepository.Object, mapper);
    }

    [Fact]
    public async Task GetEpisodes_WhenEpisodesExist_ReturnsPagedEpisodes()
    {
        // Arrange
        const int page = 1;
        const int pageSize = 5;
        const int count = 30;
        const int expectedPageCount = 6;

        var fakeEpisodes = EpisodeFaker
            .Create()
            .AsPaged(page, pageSize, count);

        var request = new GetEpisodesRequest(page, pageSize);

        _episodeRepository
            .Setup(repository => repository.GetAll(request.Page, request.PageSize, CancellationToken.None))
            .ReturnsAsync(fakeEpisodes);

        // Act
        var response = await _getEpisodesHandler.Handle(request);

        // Assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
        response.Value.Page.Should().Be(page);
        response.Value.PageSize.Should().Be(pageSize);
        response.Value.PageCount.Should().Be(expectedPageCount);
        response.Value.TotalCount.Should().Be(count);
        response.Value.HasNext.Should().BeTrue();
        response.Value.HasPrevious.Should().BeFalse();
        response.Value.Data.Should().HaveCount(pageSize);
    }

    [Fact]
    public async Task GetEpisodes_WhenEpisodesDoNotExist_ReturnsEmptyResult()
    {
        // Arrange
        const int page = 1;
        const int pageSize = 5;
        const int count = 0;
        const int expectedPageCount = 0;

        var fakeEpisodes = EpisodeFaker
            .Create()
            .AsPaged(page, pageSize, count);

        var request = new GetEpisodesRequest(page, pageSize);

        _episodeRepository
            .Setup(repository => repository.GetAll(request.Page, request.PageSize, CancellationToken.None))
            .ReturnsAsync(fakeEpisodes);

        // Act
        var response = await _getEpisodesHandler.Handle(request);

        // Assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
        response.Value.Page.Should().Be(page);
        response.Value.PageSize.Should().Be(pageSize);
        response.Value.PageCount.Should().Be(expectedPageCount);
        response.Value.TotalCount.Should().Be(count);
        response.Value.HasNext.Should().BeFalse();
        response.Value.HasPrevious.Should().BeFalse();
        response.Value.Data.Should().BeEmpty();
    }
}