using System.Web.Mvc;

namespace System_FishKoi.Areas.Directive
{
    public class DirectiveAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Directive";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Directive_default", "Directive/{controller}/{action}/{id}", new { area = "Directive", controller = "Directive", action = "Index", id = UrlParameter.Optional });
        }
    }

}