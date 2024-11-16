using System.Web.Mvc;

namespace System_FishKoi.Areas.Product
{
    public class ProductAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Product";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Product_Product_Product", "danh-sach-san-pham", new { area = "Product", controller = "Product", action = "Product", id = UrlParameter.Optional });
            context.MapRoute("Product_Product_Product_Insert", "danh-sach-san-pham/tao-moi", new { area = "Product", controller = "Product", action = "Product_Insert", id = UrlParameter.Optional });
            context.MapRoute("Product_Product_Product_Update", "danh-sach-san-pham/chinh-sua/{id}", new { area = "Product", controller = "Product", action = "Product_Update", id = UrlParameter.Optional });
            context.MapRoute("Product_default", "Product/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
        }
    }

}