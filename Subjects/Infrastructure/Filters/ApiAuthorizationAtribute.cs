using AuthenticationApplication.Controllers;
using AuthenticationApplication.Infrastructure.Classes;
using AuthenticationApplication.Infrastructure.DAL;
using AuthenticationApplication.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AuthenticationApplication.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiAuthorizationAttribute : AuthorizeAttribute
    {
        private readonly UserRole[] _allowedRoles;

        public ApiAuthorizationAttribute(params UserRole[] roles)
        {
            _allowedRoles = roles;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string userToken = GetUserToken(actionContext);

            var userId = int.Parse(userToken.Decrypt().Split('_')[1]);

            var user = Database.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                var baseController = actionContext.ControllerContext.Controller as BaseController;
                baseController.CurrentUserId = user.Id;
                baseController.CurrentUserRole = user.Role;

                if (_allowedRoles != null && _allowedRoles.Length > 0 && !_allowedRoles.Any(a => a == user.Role))
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(
                        HttpStatusCode.Unauthorized, Constants.AuthorizationRequired);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.Unauthorized, Constants.AuthorizationRequired);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private string GetUserToken(HttpActionContext actionContext)
        {
            AuthenticationHeaderValue authHeaderValue = actionContext.Request.Headers.Authorization;
            if (authHeaderValue != null)
            {
                if (authHeaderValue.Scheme == "Auth")
                {
                    return authHeaderValue.Parameter;
                }
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        //private App GetApp(HttpActionContext actionContext)
        //{
        //    string result = actionContext.Request.GetQueryNameValuePairs().FirstOrDefault(k => k.Key == "apiKey").Value;
        //    if (string.IsNullOrEmpty(result))
        //        return null;
        //    IQuery<App> query = QueryBase<App>.CreateQuery();
        //    return query.RepositoryQuery.FirstOrDefault(a => a.ServiceAccessKey == result);
        //}
    }
}