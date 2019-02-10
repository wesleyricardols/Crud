namespace Crud.Common.Request
{
    public class CrudRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        #region Constructor

        public CrudRequest()
        {
            this.Id = 0;
            this.FullName = null;
            this.EmailAddress = null;
            this.PhoneNumber = null;
        }

        public CrudRequest(int id)
        {
            this.Id = id;
            this.FullName = null;
            this.EmailAddress = null;
            this.PhoneNumber = null;
        }

        public CrudRequest(int id, string fullName, string emailAddress, string phoneNumber)
        {
            this.Id = id;
            this.FullName = fullName;
            this.EmailAddress = emailAddress;
            this.PhoneNumber = phoneNumber;
        }

        #endregion
    }
}
