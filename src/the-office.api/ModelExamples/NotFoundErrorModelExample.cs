using Swashbuckle.AspNetCore.Filters;
using the_office.domain.Enums;
using the_office.domain.Response;

namespace the_office.api.ModelExamples
{
    public class NotFoundErrorModelExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new ObjectResponse
            {
                Validations = new List<ValidationResponse>
                {
                    new ValidationResponse
                    {
                        Code = ErrorType.ResourceNotFound
                    }
                }
            };
        }
    }
}
