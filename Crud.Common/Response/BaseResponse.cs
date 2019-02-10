namespace Crud.Common.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        #region Constructor

        public BaseResponse()
        {

        }

        public BaseResponse(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        #endregion
    }
}
