using System.Web;
using System.Web.Optimization;

namespace System_FishKoi
{
    public class BundleConfig
    {
        //
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/assets/css/style.bundle.css",
                      "~/assets/plugins/global/plugins.bundle.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
