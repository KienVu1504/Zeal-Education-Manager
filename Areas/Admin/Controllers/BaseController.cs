using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZealEducationManager.Common;
using System.Web.Routing;

namespace ZealEducationManager.Areas.Admin.Controllers
{
    public class BaseController:Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {controller = "Login", action ="Index", Area="Admin"}));
            }
            else
            {
                if (session.UserName.ToLower() != "admin")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
                }
            }
            base.OnActionExecuting(filterContext);
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";//same as style in css
            }
            if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";//same as style in css
            }
            if (type == "success")
            {
                TempData["error"] = "alert-danger";//same as style in css
            }
        }
    }
}