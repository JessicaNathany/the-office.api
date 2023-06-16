using FizzWare.NBuilder;
using Moq.AutoMock;
using System.Linq.Expressions;
using the_office.api.application.Characters.Handlers;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.api.test.Application.Episodes.Fakes;
using the_office.domain.Entities;
using the_office.domain.Repositories;

namespace the_office.api.test.Application.Characters.Handlers
{
    [Collection("the-office")]
    public class RegisterCharacterHandlerTest
    {
        private readonly AutoMocker _autoMocker;

        public RegisterCharacterHandlerTest()
        {
            _autoMocker = new AutoMocker();
        }

        [Fact]
        public async Task ShouldBeInsertCharacter_Success()
        {
            var character = new Character();
            character = null;

            var request = Builder<RegisterCharacterRequest>
                .CreateNew().With(x => x.Gender = "Male").Build();

            var commandHandler = _autoMocker.CreateInstance<RegisterCharacterHandler>();

            var characterRepositoryMock = _autoMocker.GetMock<ICharacterRepository>();

            characterRepositoryMock.Setup(character => character.Any(
                    It.IsAny<Expression<Func<Character, bool>>>(), CancellationToken.None)).
                ReturnsAsync(false);

            var expectedResponse = Builder<CharacterResponse>
               .CreateNew()
               .With(x => x.Gender = "Male")
               .With(x => x.ImageUrl = "https://theoffice-uploads.s3.us-east-2.amazonaws.com/gabe.jpg")
               .With(x => x.Name = "Gabe")
               .Build();

            var mockMapper = _autoMocker.GetMock<IMapper>();

            mockMapper.Setup(mapper => mapper
            .Map<CharacterResponse>(It.Is<RegisterCharacterRequest>(c => c == request)))
            .Returns(expectedResponse);

            var unitOfWorkMock = _autoMocker.GetMock<IUnitOfWork>();

            unitOfWorkMock.Setup(uow => uow.Commit(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            //Action
            var result = await commandHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FailureInsertCharacter_CharacterExist_Success()
        {
            var character = Builder<Character>.CreateNew().With(x => x.Gender = "Male").Build();

            var request = Builder<RegisterCharacterRequest>
                .CreateNew()
                .With(x => x.Gender = "Male")
                .With(x => x.Name = "Gabe")
                .Build();

            var commandHandler = _autoMocker.CreateInstance<RegisterCharacterHandler>();

            var characterRepositoryMock = _autoMocker.GetMock<ICharacterRepository>();
            characterRepositoryMock.Setup(character => character.Any(
                It.IsAny<Expression<Func<Character, bool>>>(), It.IsAny<CancellationToken>())).
                ReturnsAsync(true);

            //Action
            var result = await commandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.IsFailure);
        }
    }
}
