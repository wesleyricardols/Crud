using System;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using Crud.Common.Exception;
using Crud.Common.Request;
using Crud.Common.Response;
using Crud.Web.Models;
using Newtonsoft.Json;

namespace Crud.Web.Controllers
{
    public class CrudController : BaseController
    {
        #region Gets

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(new CrudRequest()), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = _client.PostAsync("api/Crud/GetRecords", content).Result;
                string data = httpResponse.Content.ReadAsStringAsync().Result;

                CrudResponse response = JsonConvert.DeserializeObject<CrudResponse>(data);
                CrudModel model = new CrudModel(response.Cruds);

                return View(model);
            }
            catch (CrudException ex)
            {

            }
            catch (Exception ex)
            {

            }

            return View();
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? Id = null)
        {
            CrudModel model = null;

            if (!Id.HasValue || Id.Value == 0)
                return RedirectToAction("Index");

            CrudRequest request = new CrudRequest(Id.Value);

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _client.PostAsync("api/Crud/GetRecord", content).Result;
            string data = httpResponse.Content.ReadAsStringAsync().Result;

            CrudResponse response = JsonConvert.DeserializeObject<CrudResponse>(data);

            if (response.Success)
            {
                model = new CrudModel(response.Crud);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? Id = null)
        {
            CrudModel model = null;

            if (!Id.HasValue || Id.Value == 0)
                return RedirectToAction("Index");

            CrudRequest request = new CrudRequest(Id.Value);

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _client.PostAsync("api/Crud/GetRecord", content).Result;
            string data = httpResponse.Content.ReadAsStringAsync().Result;

            CrudResponse response = JsonConvert.DeserializeObject<CrudResponse>(data);

            if (response.Success)
            {
                model = new CrudModel(response.Crud);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? Id = null)
        {
            if (!Id.HasValue || Id.Value == 0)
                return RedirectToAction("Index");

            CrudRequest request = new CrudRequest(Id.Value);

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = _client.PostAsync("api/Crud/Delete", content).Result;
            string data = httpResponse.Content.ReadAsStringAsync().Result;

            BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(data);

            return RedirectToAction("Index");
        }

        #endregion

        #region Posts

        [HttpPost]
        public ActionResult Create(CrudModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                CrudRequest request = new CrudRequest(model.Id, model.FullName, model.EmailAddress, model.PhoneNumber);

                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = _client.PostAsync("api/Crud/Create", content).Result;
                string data = httpResponse.Content.ReadAsStringAsync().Result;

                BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(data);
            }
            catch (CrudException ex)
            {
            }
            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(CrudModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                CrudRequest request = new CrudRequest(model.Id, model.FullName, model.EmailAddress, model.PhoneNumber);

                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = _client.PostAsync("api/Crud/Edit", content).Result;
                string data = httpResponse.Content.ReadAsStringAsync().Result;

                BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(data);
            }
            catch (CrudException ex)
            {

            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }
        
        #endregion
    }
}