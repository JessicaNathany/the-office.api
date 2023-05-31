using Moq.AutoMock;
using the_office.api.application.Characters.Messaging.Requests;
using the_office.api.application.Characters.Handlers;
using Moq;

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
        public async Task ShouldBeInsertCharacter_Success() // refactoring
        {
            var request = new RegisterCharacterRequest
            {
                Gender = "Male",
                ImageUrl = "https://theoffice-uploads.s3.us-east-2.amazonaws.com/gabe.jpg",
                Name = "Gabe",
                NameActor = "Zach Woods",
                //to continue..
            };

            var commandHandler = _autoMocker.CreateInstance<RegisterCharacterHandler>();

            //Action
            var result = await commandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
