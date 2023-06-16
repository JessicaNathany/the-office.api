using the_office.api.application.Characters.Handlers;
using the_office.api.application.Common.Mappings;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.test.Application.Common.Fakes;
using the_office.domain.Repositories;
using the_office.api.application.Episodes.Handlers;
using the_office.infrastructure.Data.Repositories;

namespace the_office.api.test.Application.Characters.Handlers
{
    [Collection("the-office")]
    public class GetCharactersHandlerTest
    {
        private readonly Mock<ICharacterRepository> _characterRepository = new();
        private readonly GetCharactersHandler _getCharactersHandler;

        public GetCharactersHandlerTest()
        {
            IConfigurationProvider configuration = new MapperConfiguration(config => config.AddProfile<MappingProfile>());
            var mapper = configuration.CreateMapper();

            _getCharactersHandler = new GetCharactersHandler(_characterRepository.Object, mapper);
        }

        [Fact]
        public async Task GetCharacters_WhenCharactersExist_ReturnsPagedCharacters()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 5;
            const int count = 30;
            const int expectedPageCount = 6;

            var fakeCharacters = CharacterFaker.Create().AsPaged(page, pageSize, count);

            var request = new GetCharactersRequest(page, pageSize);

            _characterRepository
                .Setup(repository => repository.GetAll(request.Page, request.PageSize, CancellationToken.None))
                .ReturnsAsync(fakeCharacters);

            // Act
            var response = await _getCharactersHandler.Handle(request, CancellationToken.None);

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
        public async Task GetCharacters_WhenCharactersDoNotExist_ReturnsEmptyResult()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 5;
            const int count = 0;
            const int expectedPageCount = 0;

            var fakeCharacters = CharacterFaker.Create().AsPaged(page, pageSize, count);

            var request = new GetCharactersRequest(page, pageSize);

            _characterRepository
                .Setup(repository => repository.GetAll(request.Page, request.PageSize, CancellationToken.None))
                .ReturnsAsync(fakeCharacters);

            // Act
            var response = await _getCharactersHandler.Handle(request, CancellationToken.None);

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
}
