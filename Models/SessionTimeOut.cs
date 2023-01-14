using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.HttpContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DOTNETCOREEXAMPLE.Models
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           // HttpContext ctx=null;
          //  SessionExtension
           // HttpContext.Session.GetString("UserId");
          //  if(HttpContext.Session.GetString("UserId")==null)
                if (filterContext.HttpContext.Session.GetString("UserId") == null)
            {
                //filterContext.Result = new RedirectResult("~/");
                filterContext.Result =
         new RedirectToRouteResult(new RouteValueDictionary
           {
             { "action", "Index" },
            { "controller", "Home" },
            { "returnUrl",""}
            });
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
