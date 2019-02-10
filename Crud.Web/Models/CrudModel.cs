using Crud.Common.Request;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Crud.Web.Models
{
    public class CrudModel
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "O Nome deve conter no máximo 100 caracteres!")]
        [Required(ErrorMessage = "Favor preencher o campo Nome Completo!")]
        [DisplayName("Nome Completo")]
        public string FullName { get; set; }

        [StringLength(100, ErrorMessage = "O E-mail deve conter no máximo 100 caracteres!")]
        [EmailAddress(ErrorMessage = "O E-mail informado não atende o formato esperado, favor verificar!")]
        [Required(ErrorMessage = "Favor preencher o campo E-mail!")]
        [DisplayName("E-mail")]
        public string EmailAddress { get; set; }

        [StringLength(15, ErrorMessage = "O Nome deve conter no máximo 15 caracteres!")]
        [Required(ErrorMessage = "Favor preencher o campo Telefone!")]
        [DisplayName("Telefone")]
        public string PhoneNumber { get; set; }

        public string ErrorMessage { get; set; }
        public List<CrudRequest> Cruds { get; set; }

        #region Constructors

        public CrudModel()
        {

        }

        public CrudModel(List<CrudRequest> lstRequest)
        {
            this.Cruds = lstRequest;
        }

        public CrudModel(CrudRequest request)
        {
            this.Id = request.Id;
            this.FullName = request.FullName;
            this.EmailAddress = request.EmailAddress;
            this.PhoneNumber = request.PhoneNumber;
        }
        #endregion
    }
}