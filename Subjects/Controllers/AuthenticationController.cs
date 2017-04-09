using AuthenticationApplication.Classes;
using AuthenticationApplication.DAL;
using AuthenticationApplication.Entities.Inbound;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthenticationApplication.Controllers
{
    [RoutePrefix("api/authentication")]
    public class AuthenticationController : BaseController
    {
        [Route("gettoken")]
        [HttpPost]
        public IHttpActionResult GetToken(UserInbound userInbound)
        {
            var user = Database.Users.FirstOrDefault(u => 
                    u.Username == userInbound.Username && u.Password == userInbound.Password);

            if (user == null)
            {
                ThrowError(HttpStatusCode.InternalServerError, "Wrong username or password!");
            }

            var token = Guid.NewGuid().ToString() + "_" + user.Id;
            var cryptedToken = token.Crypt();
            return Ok(cryptedToken);
        }
    }
}
