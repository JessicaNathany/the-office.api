using the_office.domain.Factories;
using the_office.domain.Response;

namespace the_office.api.application.Factories
{
    public class ReponseFactory : IResponseFactory
    {
        public ObjectResponse CreateCollectionFromData(object data, int totalItems, bool paggin = false, int? currentPage = null, int? itemsPage = null)
        {
            throw new NotImplementedException();
        }

        public ObjectResponse CreateCollectionFromList<T>(IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }

        public ObjectResponse CreateCollectionFromValidations(params ValidationResponse[] validations)
        {
            throw new NotImplementedException();
        }

        public ObjectResponse CreateCollectionValidation(List<ValidationResponse> validations)
        {
            throw new NotImplementedException();
        }

        public ObjectResponse CreateObject()
        {
            throw new NotImplementedException();
        }

        public ObjectResponse CreateObjectFromData(object data)
        {
            throw new NotImplementedException();
        }

        public ObjectResponse CreateObjectFromDataValidations(object data, List<ValidationResponse> validations)
        {
            throw new NotImplementedException();
        }

        public ObjectResponse CreateObjectFromOneOrManyValydations(params ValidationResponse[] validations)
        {
            throw new NotImplementedException();
        }

        public ObjectResponse CreateObjectFromValidations(List<ValidationResponse> validations)
        {
            throw new NotImplementedException();
        }
    }
}
