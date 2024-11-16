using System.Web.Mvc;
using System_FishKoi.Controllers;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.SystemUser.Intputs;
using System_FishKoi.Domain.BUS.SystemUser;

namespace System_FishKoi.Areas.SystemUser
{
    [AuthorizeByRole]
    public class SystemUserController : BaseController
    {
        private readonly SystemUserBUS _systemUserBUS = null;
        public SystemUserController()
        {
            _systemUserBUS = new SystemUserBUS();
        }

        public ActionResult SystemUser()
        {
            return View();
        }

        public JsonResult GetPagedList(string keySearch, int draw, int pageSize, int offset)
        {
            var results = _systemUserBUS.GetPagedList(keySearch, pageSize, offset);
            _reponseMessage = _systemUserBUS.GetReponseMessage();
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

        [HttpPost]
        public JsonResult Insert(InsertSystemUser_Input bo)
        {
            int result = _systemUserBUS.Insert(bo);
            _reponseMessage = _systemUserBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(UpdateSystemUser_Input bo)
        {
            int result = _systemUserBUS.Update(bo);
            _reponseMessage = _systemUserBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateStatus(UpdateStatusSystemUser_Input bo)
        {
            int result = _systemUserBUS.UpdateStatus(bo);
            _reponseMessage = _systemUserBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(DeleteSystemUser_Input bo)
        {
            int result = _systemUserBUS.Delete(bo);
            _reponseMessage = _systemUserBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }
    }
}