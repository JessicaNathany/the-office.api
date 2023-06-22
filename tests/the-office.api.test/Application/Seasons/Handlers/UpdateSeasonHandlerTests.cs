using AutoFixture;
using the_office.api.application.Seasons.Handlers;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Seasons.Handlers;

public sealed class UpdateSeasonHandlerTests : BaseTest
{
    private readonly Mock<ISeasonRepository> _seasonRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly UpdateSeasonHandler _updateSeasonHandler;

    public UpdateSeasonHandlerTests()
    {
        _updateSeasonHandler = new UpdateSeasonHandler(_seasonRepository.Object, _unitOfWork.Object, Mapper);
    }

    [Fact]
    public async Task UpdateSeason_WhenRequestIsValid_ShouldUpdateAndReturnMapperSeasonResponse()
    {
        // Arrange
        var request = Fixture.Create<UpdateSeasonRequest>();
        var season = Fixture.Create<Season>();

        _seasonRepository.Setup(repo => repo.GetById(request.Id, default))
            .ReturnsAsync(season);
        
        // Act
        var result = await _updateSeasonHandler.Handle(request);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().NotBe(0);
        result.Value.Number.Should().Be(request.Number);
        result.Value.Title.Should().Be(request.Title);
        result.Value.TotalEpisodes.Should().Be(request.TotalEpisodes);
        result.Value.ReleaseDate.Should().Be(request.ReleaseDate);
        result.Value.Summary.Should().Be(request.Summary);
        
        _seasonRepository.Verify(repo => repo.Update(It.IsAny<Season>()), Times.Once);
        _unitOfWork.Verify(uow => uow.SaveChangesAsync(default), Times.Once);
    }
    
    [Fact]
    public async Task UpdateSeason_WhenSeasonDoNotExist_ShouldReturnFailureResultWithNotFoundError()
    {
        // Arrange
        var request = Fixture.Create<UpdateSeasonRequest>();
        Season? season = null;

        _seasonRepository.Setup(repo => repo.GetById(request.Id, default))
            .ReturnsAsync(season);
        
        // Act
        var result = await _updateSeasonHandler.Handle(request);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(SeasonError.NotFound);
        
        _seasonRepository.Verify(repo => repo.Update(It.IsAny<Season>()), Times.Never);
        _unitOfWork.Verify(uow => uow.SaveChangesAsync(default), Times.Never);
    }
}