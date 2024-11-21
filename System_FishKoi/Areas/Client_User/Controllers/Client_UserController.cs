using System.Web.Mvc;
using System_FishKoi.Controllers;
using System_FishKoi.Domain.BO.Client_User.Intputs;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BUS.Client_User;

namespace System_FishKoi.Areas.Client_User
{
    [AuthorizeByRole]
    public class Client_UserController : BaseController
    {
        private readonly Client_UserBUS _client_UserBUS = null;
        public Client_UserController()
        {
            _client_UserBUS = new Client_UserBUS();
        }

        public ActionResult Client_User()
        {
            return View();
        }

        public JsonResult GetPagedList(string keySearch, int draw, int pageSize, int offset)
        {
            var results = _client_UserBUS.GetPagedList(keySearch, pageSize, offset);
            _reponseMessage = _client_UserBUS.GetReponseMessage();
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
        public JsonResult UpdateStatus(UpdateStatusClient_User_Input bo)
        {
            int result = _client_UserBUS.UpdateStatus(bo);
            _reponseMessage = _client_UserBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(DeleteClient_User_Input bo)
        {
            int result = _client_UserBUS.Delete(bo);
            _reponseMessage = _client_UserBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }
    }
}