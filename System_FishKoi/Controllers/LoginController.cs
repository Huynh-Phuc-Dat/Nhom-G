using System.Web.Mvc;
using System_FishKoi.Domain.BO.Client_User.Intputs;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.SystemUser.Intputs;
using System_FishKoi.Domain.BO.SystemUser.Outputs;
using System_FishKoi.Domain.BUS.Client_User;
using System_FishKoi.Domain.BUS.SystemUser;

namespace System_FishKoi.Controllers
{

    // controller dang nhap
    /// <summary>
    /// trang dang nhap
    /// </summary>
    public class LoginController : Controller
    {
        public ResponseMessage _responseMessage = new ResponseMessage();
        private readonly SystemUserBUS _systemUserBUS = null;
        private readonly Client_UserBUS _client_UserBUS = null;

        public LoginController()
        {
            _responseMessage.Status = MessageStatus.Success;
            _responseMessage.Message = string.Empty;
            _systemUserBUS = new SystemUserBUS();
            _client_UserBUS = new Client_UserBUS();
        }
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        /// <summary>
        /// ham dang nhap
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public JsonResult SignIn(SignInSystemUser_Input bo) 
        {
            GetSystemUser_Output result = null;
            result = _systemUserBUS.SignIn(bo);
            _responseMessage = _systemUserBUS.GetReponseMessage();
            if (result != null)
            {
                result.IsAdmin = true;
                Session.Add("access_token", result);
                Session.Timeout = 300;
            }
            else
            {
                result = _client_UserBUS.SignIn(bo);    
                _responseMessage = _client_UserBUS.GetReponseMessage();
                if (result != null)
                {
                    if (result.Status != 1)
                    {
                        _responseMessage.Status = MessageStatus.Warning;
                        _responseMessage.Message = "Tài khoản thành viên đang chờ duyệt vui lòng thao tác lại sau";
                    }
                    else
                    {
                        result.IsAdmin = false;
                        Session.Add("access_token", result);
                        Session.Timeout = 300;
                    }
                }
            }

            _responseMessage.Data = result;
            return Json(_responseMessage, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// ham dang kí thành viên
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult Register(RegisterClient_User_Input bo) 
        {
            var result = _client_UserBUS.Register(bo);
            _responseMessage = _client_UserBUS.GetReponseMessage();
            _responseMessage.Data = result;

            return Json(_responseMessage, JsonRequestBehavior.AllowGet);

        }
    }
}