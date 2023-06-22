using AutoFixture;
using the_office.api.application.Seasons.Handlers;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Seasons.Handlers;

public sealed class RemoveSeasonHandlerTests : BaseTest
{
    private readonly Mock<ISeasonRepository> _seasonRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly RemoveSeasonHandler _removeSeasonHandler;

    public RemoveSeasonHandlerTests()
    {
        _removeSeasonHandler = new RemoveSeasonHandler(_seasonRepository.Object, _unitOfWork.Object);
    }
    
    [Fact]
    public async Task RemoveSeason_WhenSeasonExist_ShouldRemoveAndSuccessfullyResponse()
    {
        // Arrange
        var request = Fixture.Create<RemoveSeasonRequest>();
        var season = Fixture.Create<Season>();

        _seasonRepository.Setup(repo => repo.GetById(request.Id, default))
            .ReturnsAsync(season);
        
        // Act
        var result = await _removeSeasonHandler.Handle(request);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        
        _seasonRepository.Verify(repo => repo.Remove(It.IsAny<Season>()), Times.Once);
        _unitOfWork.Verify(uow => uow.SaveChangesAsync(default), Times.Once);
    }
    
    [Fact]
    public async Task RemoveSeason_WhenSeasonDoNotExist_ShouldReturnFailureResultWithNotFoundError()
    {
        // Arrange
        var request = Fixture.Create<RemoveSeasonRequest>();
        Season? season = null;

        _seasonRepository.Setup(repo => repo.GetById(request.Id, default))
            .ReturnsAsync(season);
        
        // Act
        var result = await _removeSeasonHandler.Handle(request);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(SeasonError.NotFound);
        
        _seasonRepository.Verify(repo => repo.Remove(It.IsAny<Season>()), Times.Never);
        _unitOfWork.Verify(uow => uow.SaveChangesAsync(default), Times.Never);
    }
}