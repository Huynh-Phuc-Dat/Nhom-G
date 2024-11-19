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

        
    }
}