using FizzWare.NBuilder;
using Moq.AutoMock;
using System.Linq.Expressions;
using System.Threading;
using the_office.api.application.Characters.Handlers;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.domain.Entities;
using the_office.domain.Repositories;
using the_office.infrastructure.Data.Repositories;

namespace the_office.api.test.Application.Characters.Handlers
{
    [Collection("the-office")]
    public class UpdateCharacterHandlerTests
    {
        private readonly AutoMocker _autoMocker;

        public UpdateCharacterHandlerTests()
        {
            _autoMocker = new AutoMocker();
        }

        [Fact]
        public async Task ShouldBeUpdateCharacter_Success()
        {
            var character = Builder<Character>
                .CreateNew()
                .With(c=> c.Name = "Mike")
                .With(c => c.NameActor = "Steve Carrel")
                .With(c => c.Job = "Boss")
                .With(c => c.Status = true)
                .Build();

            var request = Builder<UpdateCharacterRequest>
                .CreateNew()
                .With(x => x.Name = "Joe")
                .With(x => x.NameActor = "Glabe")
                .Build();

            var commandHandler = _autoMocker.CreateInstance<UpdateCharacterHandler>();

            var characterRepositoryMock = _autoMocker.GetMock<ICharacterRepository>();

            characterRepositoryMock.Setup(character => character.GetById(request.Id, CancellationToken.None)).
               ReturnsAsync(character);

            var expectedResponse = Builder<CharacterResponse>
               .CreateNew()
               .With(x => x.Gender = "Male")
               .With(x => x.ImageUrl = "https://theoffice-uploads.s3.us-east-2.amazonaws.com/gabe.jpg")
               .With(x => x.Name = "Gabe")
               .Build();

            var mockMapper = _autoMocker.GetMock<IMapper>();

            mockMapper.Setup(mapper => mapper
            .Map<CharacterResponse>(It.Is<UpdateCharacterRequest>(c => c == request)))
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
        public async Task FailureUpdateCharacter_CharacterExist_Success()
        {
            var character = new Character();
            character = null;

            var request = Builder<UpdateCharacterRequest>
                .CreateNew()
                .With(x => x.Name = "Joe")
                .With(x => x.NameActor = "Glabe")
                .Build();

            var commandHandler = _autoMocker.CreateInstance<UpdateCharacterHandler>();

            var characterRepositoryMock = _autoMocker.GetMock<ICharacterRepository>();

            characterRepositoryMock.Setup(character => character.GetById(request.Id, CancellationToken.None)).
               ReturnsAsync(character);

            var expectedResponse = Builder<CharacterResponse>
               .CreateNew()
               .With(x => x.Gender = "Male")
               .With(x => x.ImageUrl = "https://theoffice-uploads.s3.us-east-2.amazonaws.com/gabe.jpg")
               .With(x => x.Name = "Gabe")
               .Build();

            var unitOfWorkMock = _autoMocker.GetMock<IUnitOfWork>();

            //Action
            var result = await commandHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
        }
    }
}
