using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using the_office.domain.Response;

namespace the_office.domain.Factories
{
    public interface IResponseFactory
    {
        ObjectResponse CreateObject();

        ObjectResponse CreateObjectFromData(object data);

        ObjectResponse CreateObjectFromDataValidations(object data, List<ValidationResponse> validations);

        ObjectResponse CreateObjectFromValidations(List<ValidationResponse> validations);

        ObjectResponse CreateObjectFromOneOrManyValydations(params ValidationResponse[] validations);

        ObjectResponse CreateCollectionFromData(object data, int totalItems, bool paggin = false, int? currentPage = null, int? itemsPage = null);

        ObjectResponse CreateCollectionFromList<T>(IEnumerable<T> data);

        ObjectResponse CreateCollectionValidation(List<ValidationResponse> validations);

        ObjectResponse CreateCollectionFromValidations(params ValidationResponse[] validations);
    }
}
