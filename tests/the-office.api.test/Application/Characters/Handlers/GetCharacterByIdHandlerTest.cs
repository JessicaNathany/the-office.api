using Microsoft.EntityFrameworkCore;
using the_office.api.application.Characters.Handlers;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Common.Mappings;
using the_office.api.application.Episodes.Handlers;
using the_office.api.application.Episodes.Messaging.Requests;
using the_office.api.test.Application.Common.Fakes;
using the_office.domain.Errors;
using the_office.domain.Repositories;
using the_office.infrastructure.Data.Repositories;

namespace the_office.api.test.Application.Characters.Handlers
{
    [Collection("the-office")]
    public class GetCharacterByIdHandlerTest
    {
        private readonly Mock<ICharacterRepository> _characterRepository = new();
        private readonly GetCharacterByIdHandler _getCharacterByIdHandler;
        public GetCharacterByIdHandlerTest()
        {
            IConfigurationProvider configuration = new MapperConfiguration(config => config.AddProfile<MappingProfile>());
            var mapper = configuration.CreateMapper();
            _getCharacterByIdHandler = new GetCharacterByIdHandler(_characterRepository.Object, mapper);
        }

        [Fact]
        public async Task ShouldBeReturn_WhenCharactersExists_Success()
        {
            // Arrange
            var charcterFake = CharacterFaker.Create().AsQueryable();

            var character = await charcterFake.FirstOrDefaultAsync();
            var request = new GetCharacterByIdRequest(character!.Id);

            _characterRepository.Setup(repository => repository.GetQueryable()).Returns(charcterFake);

            // Act
            var response = await _getCharacterByIdHandler.Handle(request, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.IsFailure.Should().BeFalse();
            response.Value.Name.Should().Be(character.Name);
            response.Value.NameActor.Should().Be(character.NameActor);
            response.Value.Job.Should().Be(character.Job);
            response.Value.Gender.Should().Be(character.Gender);
            response.Value.ImageUrl.Should().Be(character.ImageUrl);
        }

        [Fact]
        public async Task ShouldBeReturn_WhenCharactersNotExists_NotFound()
        {
            // Arrange
            const int characterId = 10;
            var request = new GetCharacterByIdRequest(characterId);

            var fakeCharacter = CharacterFaker.Create().AsQueryableEmpty();

            _characterRepository.Setup(repository => repository.GetQueryable()).Returns(fakeCharacter);

            // Act
            var response = await _getCharacterByIdHandler.Handle(request, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.IsFailure.Should().BeTrue();
            response.Error.Should().Be(CharacterError.NotFound);
        }
    }
}
