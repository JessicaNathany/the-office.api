using Moq.AutoMock;
using System.Linq.Expressions;
using System.Threading;
using the_office.api.application.Characters.Handlers;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.domain.Entities;
using the_office.domain.Repositories;

namespace the_office.api.test.Mediator
{
    [Collection("the-office")]
    public class RegisterCharacterHandlerTest
    {
        private readonly AutoMocker _autoMocker;

        public RegisterCharacterHandlerTest()
        {
            _autoMocker= new AutoMocker();  
        }

        [Fact] 
        public async Task ShouldBeInsertCharacter_Success() 
        {
            var character = new Character();
            character = null;

            var request = new RegisterCharacterRequest
            {
                Gender = "Male",
                ImageUrl = "https://theoffice-uploads.s3.us-east-2.amazonaws.com/gabe.jpg",
                Name = "Gabe",
                NameActor = "Zach Woods",
                Job = "Assistent"
            };

            var commandHandler = _autoMocker.CreateInstance<RegisterCharacterHandler>();

            var characterRepositoryMock = _autoMocker.GetMock<ICharacterRepository>();
            
            characterRepositoryMock.Setup(character => character.Any(
                    It.IsAny<Expression<Func<Character, bool>>>(), CancellationToken.None)).
                ReturnsAsync(false);

            var expectedResponse = new CharacterResponse()
            {
                Job = "Assistent",
                ImageUrl = "https://theoffice-uploads.s3.us-east-2.amazonaws.com/gabe.jpg",
                Gender = "Male",
                Name = "Gabe",
                NameActor = "Zach Woods",
                Status = true
            };

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
            var character = new Character()
            {
                Id = 1,
                Code = new Guid(),
                Gender = "Male",
                Job = "Assistent",
                Name = "Gabe",
                NameActor = "Zach Woods",
                ImageUrl = "https://theoffice-uploads.s3.us-east-2.amazonaws.com/gabe.jpg",
                Status = true
            };

            var request = new RegisterCharacterRequest
            {
                Gender = "Male",
                ImageUrl = "https://theoffice-uploads.s3.us-east-2.amazonaws.com/gabe.jpg",
                Name = "Gabe",
                NameActor = "Zach Woods",
            };

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
