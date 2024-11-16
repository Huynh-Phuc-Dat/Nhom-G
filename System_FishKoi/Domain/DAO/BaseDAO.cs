using System.Web;
using System_FishKoi.Domain.BO.SystemUser.Outputs;

namespace System_FishKoi.Domain.DAO
{
    public class BaseDAO
    {
        public static GetSystemUser_Output CurrentUser
        {
            get { return (GetSystemUser_Output)HttpContext.Current.Session["access_token"]; }
        }

        public string _userLogin = CurrentUser == null ? "admin" : CurrentUser.Username;
    }
}