using System.Web.Mvc;
using System_FishKoi.Domain.BUS.Home;

namespace System_FishKoi.Controllers
{
    public class HomeController : BaseController
    {
        private readonly HomeBUS _homeBUS = null;
        public HomeController()
        {
            _homeBUS = new HomeBUS();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SideBar()
        {
            return View();
        }

        public ActionResult SideBar_Footer()
        {
            return View();
        }

        public ActionResult Authorize()
        {
            return View();
        }

        public JsonResult GetSystem_User()
        {
            _reponseMessage.Data = CurrentUser;
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData_Total_System_FishKoi()
        {
            var result = _homeBUS.GetData_Total_System_FishKoi();
            _reponseMessage = _homeBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData_Lake_Total_FishKoi()
        {
            var result = _homeBUS.GetData_Lake_Total_FishKoi();
            _reponseMessage = _homeBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData_AVG_Lake_Setting(int lakeID)
        {
            var results = _homeBUS.GetData_AVG_Lake_Setting(lakeID);
            _reponseMessage = _homeBUS.GetReponseMessage();
            _reponseMessage.Data = results;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

    }
}