using System.Linq.Expressions;
using AutoFixture;
using the_office.api.application.Seasons.Handlers;
using the_office.api.application.Seasons.Messaging.Requests;
using the_office.domain.Entities;
using the_office.domain.Errors;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Seasons;

public sealed class RegisterSeasonHandlerTests : BaseTest
{
    private readonly Mock<ISeasonRepository> _seasonRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly RegisterSeasonHandler _registerSeasonHandler;
    
    public RegisterSeasonHandlerTests()
    {
        _registerSeasonHandler = new RegisterSeasonHandler(_seasonRepository.Object, _unitOfWork.Object, Mapper);
    }

    [Fact]
    public async Task RegisterSeason_WhenSeasonDoesNotExist_ShouldRegisterAndReturnMappedResponse()
    {
        // Arrange
        var request = Fixture.Create<RegisterSeasonRequest>();

        _seasonRepository.Setup(repo => repo.Any(It.IsAny<Expression<Func<Season, bool>>>(), default))
            .ReturnsAsync(false);

        _seasonRepository.Setup(repo => repo.Add(It.IsAny<Season>()))
            .Callback<Season>(season => season.Id = Fixture.Create<int>());

        // Act
        var result = await _registerSeasonHandler.Handle(request);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().NotBe(0);
        result.Value.Number.Should().Be(request.Number);
        result.Value.Title.Should().Be(request.Title);
        result.Value.TotalEpisodes.Should().Be(request.TotalEpisodes);
        result.Value.ReleaseDate.Should().Be(request.ReleaseDate);
        result.Value.Summary.Should().Be(request.Summary);

        _seasonRepository.Verify(repo => repo.Add(It.IsAny<Season>()), Times.Once);
        _unitOfWork.Verify(uow => uow.SaveChangesAsync(default), Times.Once);
    }
    
    [Fact]
    public async Task RegisterSeason_WhenSeasonExists_ShouldReturnFailureResultWithAlreadyExistsError()
    {
        // Arrange
        var request = Fixture.Create<RegisterSeasonRequest>();
        _seasonRepository.Setup(repo => repo.Any(It.IsAny<Expression<Func<Season, bool>>>(), default))
            .ReturnsAsync(true);

        // Act
        var result = await _registerSeasonHandler.Handle(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(SeasonError.AlreadyExists);

        _seasonRepository.Verify(repo => repo.Add(It.IsAny<Season>()), Times.Never);
        _unitOfWork.Verify(uow => uow.SaveChangesAsync(default), Times.Never);
    }
}