using AuthenticationApplication.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthenticationApplication.Controllers
{
    public abstract class BaseController : ApiController
    {
        public int CurrentUserId { get; set; }
        public UserRole CurrentUserRole { get; set; }

        public void ThrowError(HttpStatusCode httpStatusCode, string details)
        {
            var error = new { Status = (int)httpStatusCode, Details = details };
            var message = new HttpResponseMessage(httpStatusCode) {
                Content = new StringContent(JsonConvert.SerializeObject(error))
            };
            throw new HttpResponseException(message);
        }
    }
}
