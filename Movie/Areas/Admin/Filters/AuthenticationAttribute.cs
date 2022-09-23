using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

using Movie.Models;

namespace Movie.Areas.Admin.Filters
{
    public class AuthenticationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var Id = CheckCookies(context.HttpContext.Request);
            if(Id <= -1)
                context.Result = new RedirectResult("../../Admin/AdminAuth/SingIn");
        }
        private int CheckCookies(HttpRequest request)
        {
            var Id = request.Cookies["AdminId"];
            if (Id != null)
                return int.Parse(Id);
            return -1;
        }
    }
}