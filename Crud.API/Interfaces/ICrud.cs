using Crud.Common.Request;
using Crud.Common.Validation;
using System.Collections.Generic;

namespace Crud.API.Interfaces
{
    public interface ICrud
    {
        bool Create(CrudRequest request);
        bool Edit(CrudRequest request);
        bool Delete(CrudRequest request);
        bool GetRecords(out List<CrudRequest> Cruds, CrudRequest request);
        bool GetRecord(out CrudRequest Crud, CrudRequest request);
        BaseValidation ValidateName(string name);
        BaseValidation ValidateEmail(string email);
        BaseValidation ValidatePhoneNumber(string phoneNumber);
    }
}