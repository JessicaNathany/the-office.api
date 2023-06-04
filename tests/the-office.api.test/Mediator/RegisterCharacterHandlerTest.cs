using AutoMapper;
using Moq;
using Moq.AutoMock;
using the_office.api.application.Characters.Handlers;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Messaging.Response;
using the_office.domain.Entities;
using the_office.domain.Repositories;
using Xunit;

namespace the_office.api.test.Handler
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
            characterRepositoryMock.Setup(character => character.GetByName("Gabe", "Zach Woods"))
                .ReturnsAsync(character);

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

            mockMapper.Setup(mapper => mapper.Map<CharacterResponse>(It.Is<RegisterCharacterRequest>(c => c == request)))
            .Returns(expectedResponse);

            //Action
            var result = await commandHandler.Handle(request, It.IsAny<CancellationToken>());

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
            characterRepositoryMock.Setup(character => character.GetByName(request.Name, request.NameActor)).
                ReturnsAsync(character);

            //Action
            var result = await commandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.IsFailure);
        }
    }
}
