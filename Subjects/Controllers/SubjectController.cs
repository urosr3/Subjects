using AuthenticationApplication.Infrastructure.DAL;
using AuthenticationApplication.Infrastructure.Enums;
using AuthenticationApplication.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthenticationApplication.Controllers
{
    [RoutePrefix("api/subject")]
    public class SubjectController : BaseController
    {
        [ApiAuthorization]
        [HttpGet]
        [Route("getfirst")]
        public IHttpActionResult getfirst()
        {
            var firstSubject = Database.Subjects.FirstOrDefault();
            var userId = CurrentUserId;
            var userRole = CurrentUserRole;

            return Ok(new { FirstSubject = firstSubject, UserId = userId, UserRole = userRole.ToString() });
        }

        // Only Admin can get all subjects
        [ApiAuthorization(UserRole.Admin)]
        [HttpGet]
        [Route("getall")]
        public IHttpActionResult getall()
        {
            var subjects = Database.Subjects;

            return Ok(subjects);
        }
    }
}
