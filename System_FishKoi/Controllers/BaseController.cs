using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.SystemUser.Outputs;

namespace System_FishKoi.Controllers
{
    public class BaseController : Controller
    {
        public ResponseMessage _reponseMessage = new ResponseMessage();
        protected readonly string STR_MONTH_YEAR = $"{DateTime.Now.Year}/{DateTime.Now.Month}";

        public BaseController()
        {

            //hatfawftfwfdhg
            _reponseMessage.Status = MessageStatus.Success;
            _reponseMessage.Message = string.Empty;
        }

        public GetSystemUser_Output CurrentUser
        {
            get { return (GetSystemUser_Output)Session["access_token"]; }
        }


        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session["access_token"] != null)
            {

            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login", area = "" }));
            }
        }
    }

    public class AuthorizeByRoleAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string Url = filterContext.HttpContext.Request.Url.PathAndQuery;
            if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session["access_token"] != null)
            {
                if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false) &&
                       !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                {
                    bool isAuthorize = false;
                    var user = (GetSystemUser_Output)filterContext.HttpContext.Session["access_token"];
                    isAuthorize = user.IsAdmin;

                    if (!isAuthorize)
                    {
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.Result = new Http401Result();
                        }
                        else
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Authorize", area = "", url = Url }));
                        }
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login", area = "", url = Url }));
            }
        }
    }


    internal class Http401Result : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            // Set the response code to 403.
            context.HttpContext.Response.StatusCode = 401;
        }
    }
}