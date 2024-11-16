using System.Web.Mvc;
using System_FishKoi.Controllers;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.Lake.Inputs;
using System_FishKoi.Domain.BUS.Lake;

namespace System_FishKoi.Areas.Lake
{
    public class Lake_SettingController : BaseController
    {
        private readonly Lake_SettingBUS _lake_SettingBUS = null;
        public Lake_SettingController()
        {
            _lake_SettingBUS = new Lake_SettingBUS();
        }

        public ActionResult Lake_Setting()
        {
            return View();
        }

        public ActionResult Lake_Setting_Insert()
        {
            return View();
        }

        public ActionResult Lake_Setting_Update()
        {
            return View();
        }

        public JsonResult GetPagedList(string keySearch, int lakeID, int draw, int pageSize, int offset)
        {
            var results = _lake_SettingBUS.GetPagedList(keySearch, lakeID, pageSize, offset);
            _reponseMessage = _lake_SettingBUS.GetReponseMessage();
            int recordsTotal = 0;

            int recordsFiltered = 0;
            if (_reponseMessage.Status == MessageStatus.Success)
            {
                recordsTotal = results.Count > 0 ? results[0].TotalRow : 0;
                recordsFiltered = results.Count > 0 ? results[0].TotalRow : 0;
            }

            _reponseMessage.Data = new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = results,
            };
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDetail(int settingID)
        {
            var result = _lake_SettingBUS.GetDetail(settingID);
            _reponseMessage = _lake_SettingBUS.GetReponseMessage();
            _reponseMessage.Data = result;
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insert(InsertLake_Setting_Input bo)
        {
            int result = _lake_SettingBUS.Insert(bo);
            _reponseMessage = _lake_SettingBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(UpdateLake_Setting_Input bo)
        {
            int result = _lake_SettingBUS.Update(bo);
            _reponseMessage = _lake_SettingBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(DeleteLake_Setting_Input bo)
        {
            int result = _lake_SettingBUS.Delete(bo);
            _reponseMessage = _lake_SettingBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }
    }
}