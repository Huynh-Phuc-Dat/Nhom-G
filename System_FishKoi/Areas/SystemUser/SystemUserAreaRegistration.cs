using System.Web.Mvc;

namespace System_FishKoi.Areas.SystemUser
{
    public class SystemUserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SystemUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("SystemUser_SystemUser_SystemUser", "nhan-vien-he-thong", new { area = "SystemUser", controller = "SystemUser", action = "SystemUser", id = UrlParameter.Optional });
            context.MapRoute("SystemUser_default", "SystemUser/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
        }
    }

}