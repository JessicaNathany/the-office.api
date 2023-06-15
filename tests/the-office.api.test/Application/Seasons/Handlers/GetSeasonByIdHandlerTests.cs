using AutoFixture;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using the_office.api.application.Seasons.Handlers;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Seasons.Handlers;

public sealed class GetSeasonByIdHandlerTests : BaseTest
{
    private readonly Mock<ISeasonRepository> _seasonRepository = new();
    private readonly GetSeasonByIdHandler _getSeasonByIdHandler;

    public GetSeasonByIdHandlerTests()
    {
        _getSeasonByIdHandler = new GetSeasonByIdHandler(_seasonRepository.Object, Mapper);
    }

    [Fact]
    public async Task GetSeasonById_WhenSeasonExists_ShouldReturnSeasonResponse()
    {
        // Arrange
        var seasons = Fixture.Create<List<Season>>()
            .BuildMock();
        var expectedSeason = await seasons.FirstAsync();
        var request = new GetSeasonByIdRequest(expectedSeason.Id);
        
        _seasonRepository.Setup(repo => repo.GetQueryable())
            .Returns(seasons);

        // Act
        var result = await _getSeasonByIdHandler.Handle(request);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(expectedSeason.Id);
        result.Value.Number.Should().Be(expectedSeason.Number);
        result.Value.Title.Should().Be(expectedSeason.Title);
        result.Value.TotalEpisodes.Should().Be(expectedSeason.TotalEpisodes);
        result.Value.ReleaseDate.Should().Be(expectedSeason.ReleaseDate);
        result.Value.Summary.Should().Be(expectedSeason.Summary);

        _seasonRepository.Verify(repo => repo.GetQueryable(), Times.Once);
    }

    [Fact]
    public async Task GetSeasonById_WhenSeasonDoesNotExist_ShouldReturnFailureWithNotFoundError()
    {
        // Arrange
        const int seasonId = 1;
        var request = new GetSeasonByIdRequest(seasonId);

        _seasonRepository.Setup(repo => repo.GetQueryable())
            .Returns(Enumerable.Empty<Season>().BuildMock());

        // Act
        var result = await _getSeasonByIdHandler.Handle(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(SeasonError.NotFound);

        _seasonRepository.Verify(repo => repo.GetQueryable(), Times.Once);
    }
}