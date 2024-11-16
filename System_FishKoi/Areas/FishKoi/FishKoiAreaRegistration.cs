using System.Web.Mvc;

namespace System_FishKoi.Areas.FishKoi
{
    public class FishKoiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FishKoi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("FishKoi_FishKoi_FishKoi", "danh-sach-ca-koi", new { area = "FishKoi", controller = "FishKoi", action = "FishKoi", id = UrlParameter.Optional });
            context.MapRoute("FishKoi_FishKoi_FishKoi_Insert", "danh-sach-ca-koi/tao-moi", new { area = "FishKoi", controller = "FishKoi", action = "FishKoi_Insert", id = UrlParameter.Optional });
            context.MapRoute("FishKoi_FishKoi_FishKoi_Update", "danh-sach-ca-koi/chinh-sua/{id}", new { area = "FishKoi", controller = "FishKoi", action = "FishKoi_Update", id = UrlParameter.Optional });
            context.MapRoute("FishKoi_default", "FishKoi/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
        }
    }

}