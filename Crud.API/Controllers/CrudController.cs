using Crud.API.Interfaces;
using Crud.API.Service;
using Crud.Common.Exception;
using Crud.Common.Request;
using Crud.Common.Response;
using Crud.Common.Validation;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Crud.API.Controllers
{
    public class CrudController : ApiController
    {
        #region Properties

        private readonly ICrud _crud;

        #endregion

        #region Constructor

        public CrudController()
        {
            _crud = new CrudService();
        }

        #endregion

        [Route("api/Crud/Create")]
        [HttpPost]
        public BaseResponse Create(CrudRequest request)
        {
            BaseResponse response = null;

            try
            {
                if (!_crud.ValidateName(request.FullName).Success)
                {
                    BaseValidation validateName = _crud.ValidateName(request.FullName);
                    throw new CrudException(validateName.Message);
                }

                if (!_crud.ValidateEmail(request.EmailAddress).Success)
                {
                    BaseValidation validateEmail = _crud.ValidateEmail(request.EmailAddress);
                    throw new CrudException(validateEmail.Message);
                }

                if (!_crud.ValidatePhoneNumber(request.PhoneNumber).Success)
                {
                    BaseValidation validatePhone = _crud.ValidatePhoneNumber(request.PhoneNumber);
                    throw new CrudException(validatePhone.Message);
                }
            
                if (_crud.Create(request))
                {
                    response = new BaseResponse(true, "Cadastro realizado com sucesso!");
                }
            }
            catch (CrudException ex)
            {
                response = new BaseResponse(false, ex.Message);
            }
            catch (Exception ex)
            {
                response = new BaseResponse(false, "Não foi possível realizar o cadastro. Erro: " + ex.Message);
            }

            return response;
        }

        [Route("api/Crud/Delete")]
        [HttpPost]
        public BaseResponse Delete(CrudRequest request)
        {
            BaseResponse response = null;

            try
            {
                if (_crud.Delete(request))
                    response = new BaseResponse(true, "Exclusão realizada com sucesso!");
            }
            catch (CrudException ex)
            {
                response = new BaseResponse(false, ex.Message);
            }
            catch (Exception ex)
            {
                response = new BaseResponse(false, "Não foi possível realizar a exclusão. Erro: " + ex.Message);
            }

            return response;
        }

        [Route("api/Crud/Edit")]
        [HttpPost]
        public BaseResponse Edit(CrudRequest request)
        {
            BaseResponse response = null;

            try
            {
                if (!_crud.ValidateName(request.FullName).Success)
                {
                    BaseValidation validateName = _crud.ValidateName(request.FullName);
                    throw new CrudException(validateName.Message);
                }

                if (!_crud.ValidateEmail(request.EmailAddress).Success)
                {
                    BaseValidation validateEmail = _crud.ValidateEmail(request.EmailAddress);
                    throw new CrudException(validateEmail.Message);
                }

                if (!_crud.ValidatePhoneNumber(request.PhoneNumber).Success)
                {
                    BaseValidation validatePhone = _crud.ValidatePhoneNumber(request.PhoneNumber);
                    throw new CrudException(validatePhone.Message);
                }

                if (_crud.Edit(request))
                {
                    response = new BaseResponse(true, "Alteração realizada com sucesso!");
                }
            }
            catch (CrudException ex)
            {
                response = new BaseResponse(false, ex.Message);
            }
            catch (Exception ex)
            {
                response = new BaseResponse(false, "Não foi possível realizar a alteração. Erro: " + ex.Message);
            }

            return response;
        }

        [Route("api/Crud/GetRecords")]
        [HttpPost]
        public CrudResponse GetRecords(CrudRequest request)
        {
            List<CrudRequest> lstCruds = null;
            CrudResponse response = null;

            try
            {
                if (_crud.GetRecords(out lstCruds, request))
                    response = new CrudResponse(true, "Registros recuperados com sucesso!", lstCruds);
            }
            catch (CrudException ex)
            {
                response = new CrudResponse(false, ex.Message);
            }
            catch (Exception ex)
            {
                response = new CrudResponse(false, "Não foi possível recuperar os registros. Erro: " + ex.Message);
            }

            return response;
        }

        [Route("api/Crud/GetRecord")]
        [HttpPost]
        public CrudResponse GetRecord(CrudRequest request)
        {
            CrudRequest dbCrud = null;
            CrudResponse response = null;
            
            try
            {
                if (_crud.GetRecord(out dbCrud, request))
                    response = new CrudResponse(true, "Registro recuperado com sucesso!", dbCrud);
            }
            catch (CrudException ex)
            {
                response = new CrudResponse(false, ex.Message);
            }
            catch (Exception ex)
            {
                response = new CrudResponse(false, "Não foi possível recuperar o registro. Erro: " + ex.Message);
            }

            return response;
        }
    }
}