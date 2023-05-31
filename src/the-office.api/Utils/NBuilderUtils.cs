using FizzWare.NBuilder;
using the_office.api.application.Characters.Messaging.Response;

namespace the_office.api.Utils
{
    public  static class NBuilderUtils
    {
        public static CharacterResponse CreateCharacter()
        {
            return Builder<CharacterResponse>.CreateNew().Build();
        }
    }
}
