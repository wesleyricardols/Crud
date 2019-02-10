using Crud.Common.Request;
using System.Collections.Generic;

namespace Crud.Common.Response
{
    public class CrudResponse : BaseResponse
    {
        public List<CrudRequest> Cruds { get; set; }
        public CrudRequest Crud { get; set; }

        #region Constructor

        public CrudResponse() : base()
        {

        }

        public CrudResponse(bool success, string message) : base(success, message)
        {

        }

        public CrudResponse(bool success, string message, List<CrudRequest> cruds) : base(success, message)
        {
            this.Cruds = cruds;
        }

        public CrudResponse(bool success, string message, CrudRequest crud) : base(success, message)
        {
            this.Crud = crud;
        }

        #endregion
    }
}
