using Crud.API.Extensions;
using Crud.API.Interfaces;
using Crud.Common.Exception;
using Crud.Common.Request;
using Crud.Common.Validation;
using Crud.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Crud.API.Service
{
    public class CrudService : ICrud
    {
        #region Properties

        private CrudContext _context { get; set; }

        #endregion

        #region Constructor

        public CrudService()
        {
            _context = new CrudContext();
        }

        #endregion

        public bool Create(CrudRequest request)
        {
            try
            {
                Database.Crud dbCrud = new Database.Crud()
                {
                    FullName = request.FullName,
                    EmailAddress = request.EmailAddress,
                    PhoneNumber = request.PhoneNumber
                };

                _context.Cruds.Add(dbCrud);
                _context.SaveChanges();

                if (dbCrud.CrudId > 0)
                    return true;
            }
            catch (CrudException ex)
            {
                throw new CrudException("Ocorreu um erro no Cadastro. Erro: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }
        public bool Edit(CrudRequest request)
        {
            Database.Crud dbCrud = null;

            try
            {
                dbCrud = _context.Cruds.FirstOrDefault(__crud => __crud.CrudId.Equals(request.Id));

                if (dbCrud == null || dbCrud.CrudId == 0)
                    return false;

                dbCrud.FullName = request.FullName;
                dbCrud.EmailAddress = request.EmailAddress;
                dbCrud.PhoneNumber = request.PhoneNumber;

                _context.SaveChanges();

                return true;
            }
            catch (CrudException ex)
            {
                throw new CrudException("Ocorreu um erro na Alteração. Erro: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(CrudRequest request)
        {
            Database.Crud dbCrud = null;

            try
            {
                dbCrud = _context.Cruds.FirstOrDefault(__crud => __crud.CrudId.Equals(request.Id));

                if (dbCrud == null || dbCrud.CrudId == 0)
                    return false;

                _context.Cruds.Remove(dbCrud);

                _context.SaveChanges();

                return true;
            }
            catch (CrudException ex)
            {
                throw new CrudException("Ocorreu um erro na Alteração. Erro: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool GetRecords(out List<CrudRequest> Cruds, CrudRequest request)
        {
            List<Database.Crud> lstCruds = null;

            try
            {
                lstCruds = _context.Cruds
                    .FilterCruds(request)
                    .OrderBy(__crud => __crud.FullName)
                    .ToList();

                if (lstCruds == null || !lstCruds.Any())
                    throw new CrudException("Não há registros a serem exibidos!");

                Cruds = lstCruds.Select(__crud => new CrudRequest(__crud.CrudId, __crud.FullName, __crud.EmailAddress, __crud.PhoneNumber)).ToList();

                return true;
            }
            catch(CrudException ex)
            {
                throw new CrudException("Ocorreu um erro ao recuperar os registros do banco de dados. Erro: " + ex.Message);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool GetRecord(out CrudRequest Crud, CrudRequest request)
        {
            Database.Crud dbCrud = null;

            try
            {
                dbCrud = _context.Cruds
                    .FilterCruds(request)
                    .OrderBy(__crud => __crud.FullName)
                    .FirstOrDefault();

                if (dbCrud == null || dbCrud.CrudId == 0)
                    throw new CrudException("O Registro solicitado não existe!");

                Crud = new CrudRequest(dbCrud.CrudId, dbCrud.FullName, dbCrud.EmailAddress, dbCrud.PhoneNumber);

                return true;
            }
            catch (CrudException ex)
            {
                throw new CrudException(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #region Validation

        public BaseValidation ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new BaseValidation(false, "Favor preencher o campo Nome Completo!");

            if (name.Length > 100)
                return new BaseValidation(false, "O Nome deve conter no máximo 100 caracteres!");

            return new BaseValidation(true);
        }
        public BaseValidation ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new BaseValidation(false, "Favor preencher o campo E-mail!");

            if (email.Length > 100)
                return new BaseValidation(false, "O E-mail deve conter no máximo 100 caracteres!");

            if (!ValidateEmailAddress(email))
                return new BaseValidation(false, "O E-mail informado não atende o formato esperado, favor verificar!");

            return new BaseValidation(true);
        }
        public BaseValidation ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return new BaseValidation(false, "Favor preencher o campo Telefone!");

            if (phoneNumber.Length > 15)
                return new BaseValidation(false, "O Nome deve conter no máximo 15 caracteres!");
            
            return new BaseValidation(true);
        }
        
        #endregion
        
        #region Private Methods

        private bool ValidateEmailAddress(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}