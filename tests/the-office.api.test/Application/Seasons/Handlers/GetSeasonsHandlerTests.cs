using AutoFixture;
using MockQueryable.Moq;
using the_office.api.application.Seasons.Handlers;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.domain.Entities;
using the_office.domain.Repositories;
using the_office.infrastructure.Data.Pagination;

namespace the_office.api.test.Application.Seasons.Handlers;

public sealed class GetSeasonsHandlerTests : BaseTest
{
    private readonly Mock<ISeasonRepository> _seasonRepository = new();
    private readonly GetSeasonsHandler _getSeasonsHandler;

    public GetSeasonsHandlerTests()
    {
        _getSeasonsHandler = new GetSeasonsHandler(_seasonRepository.Object, Mapper);
    }

    [Fact]
    public async Task GetSeasons_WhenSeasonsExist_ShouldReturnPagedResultOfSeasonResponse()
    {
        // Arrange
        var request = new GetSeasonsRequest(1, 10);
        var seasons = Fixture.CreateMany<Season>(30)
            .BuildMock();
        
        var pagedSeasons = new PagedList<Season>(seasons, request.Page, request.PageSize);

        _seasonRepository.Setup(repository => repository.GetAll(request.Page, request.PageSize, default))
            .ReturnsAsync(pagedSeasons);
        
        // Act
        var response = await _getSeasonsHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
        response.Value.Page.Should().Be(request.Page);
        response.Value.PageSize.Should().Be(request.PageSize);
        response.Value.PageCount.Should().Be(pagedSeasons.PageCount);
        response.Value.TotalCount.Should().Be(pagedSeasons.TotalCount);
        response.Value.HasNext.Should().BeTrue();
        response.Value.HasPrevious.Should().BeFalse();
        response.Value.Data.Should().HaveCount(request.PageSize);
        
        _seasonRepository.Verify(repository => repository.GetAll(request.Page, request.PageSize, default), Times.Once);
    }
    
    [Fact]
    public async Task GetSeasons_WhenSeasonsDoNotExist_ShouldReturnEmptyPagedResult()
    {
        // Arrange
        var request = new GetSeasonsRequest(1, 10);
        var seasons = Fixture.CreateMany<Season>(0)
            .BuildMock();
        
        var pagedSeasons = new PagedList<Season>(seasons, request.Page, request.PageSize);

        _seasonRepository.Setup(repository => repository.GetAll(request.Page, request.PageSize, default))
            .ReturnsAsync(pagedSeasons);
        
        // Act
        var response = await _getSeasonsHandler.Handle(request);
        
        // Assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
        response.Value.Page.Should().Be(request.Page);
        response.Value.PageSize.Should().Be(request.PageSize);
        response.Value.PageCount.Should().Be(pagedSeasons.PageCount);
        response.Value.TotalCount.Should().Be(pagedSeasons.TotalCount);
        response.Value.HasNext.Should().BeFalse();
        response.Value.HasPrevious.Should().BeFalse();
        response.Value.Data.Should().BeEmpty();
    }
}