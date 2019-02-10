namespace Crud.Common.Validation
{
    public class BaseValidation
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        #region Constructor

        public BaseValidation(bool success)
        {
            this.Success = success;
        }

        public BaseValidation(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        #endregion
    }
}
