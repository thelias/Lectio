using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LectioServer.Filters
{
    public class RequireWebApiHttpAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var ac = actionContext;

            base.OnAuthorization(actionContext);
        }
    }
}