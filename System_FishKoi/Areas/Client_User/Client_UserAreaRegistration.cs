using System.Web.Mvc;

namespace System_FishKoi.Areas.Client_User
{
    public class Client_UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Client_User";
            }
        }


        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Client_User_Client_User_Client_User", "thanh-vien-dang-ky", new { area = "Client_User", controller = "Client_User", action = "Client_User", id = UrlParameter.Optional });
            context.MapRoute("Client_User_default", "Client_User/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
        }
    }

}