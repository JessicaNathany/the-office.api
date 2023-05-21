using FizzWare.NBuilder;
using the_office.api.application.Characters.Responses;

namespace the_office.api.Utils
{
    public  static class NBuilderUtils
    {
        public static CreateCharacterResponse CreateCharacter()
        {
            return Builder<CreateCharacterResponse>.CreateNew().Build();
        }
    }
}
