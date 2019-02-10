using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Crud.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Properties

        protected HttpClient _client { get; set; }

        #endregion

        #region Constructor

        public BaseController()
        {
            if (_client != null)
                return;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(Properties.Settings.Default.APIServerAddress);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        #endregion
    }
}