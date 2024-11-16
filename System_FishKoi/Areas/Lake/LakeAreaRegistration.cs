using System.Web.Mvc;

namespace System_FishKoi.Areas.Lake
{
    public class LakeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Lake";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Lake_Lake_Setting_Lake_Setting", "thong-so-nuoc", new { area = "Lake", controller = "Lake_Setting", action = "Lake_Setting", id = UrlParameter.Optional });
            context.MapRoute("Lake_Lake_Setting_Lake_Setting_Insert", "thong-so-nuoc/thiet-lap", new { area = "Lake", controller = "Lake_Setting", action = "Lake_Setting_Insert", id = UrlParameter.Optional });
            context.MapRoute("Lake_Lake_Setting_Lake_Setting_Update", "thong-so-nuoc/chinh-sua/{id}", new { area = "Lake", controller = "Lake_Setting", action = "Lake_Setting_Update", id = UrlParameter.Optional });

            context.MapRoute("Lake_Lake_Lake", "danh-sach-ho-ca", new { area = "Lake", controller = "Lake", action = "Lake", id = UrlParameter.Optional });
            context.MapRoute("Lake_Lake_Lake_Insert", "danh-sach-ho-ca/tao-moi", new { area = "Lake", controller = "Lake", action = "Lake_Insert", id = UrlParameter.Optional });
            context.MapRoute("Lake_Lake_Lake_Update", "danh-sach-ho-ca/chinh-sua/{id}", new { area = "Lake", controller = "Lake", action = "Lake_Update", id = UrlParameter.Optional });

            context.MapRoute("Lake_default", "Lake/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
        }
    }

}