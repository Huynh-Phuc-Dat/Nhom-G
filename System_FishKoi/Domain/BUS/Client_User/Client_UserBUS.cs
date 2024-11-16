using System;
using System.Collections.Generic;
using System_FishKoi.Domain.BO.Client_User.Intputs;
using System_FishKoi.Domain.BO.Client_User.Outputs;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.SystemUser.Intputs;
using System_FishKoi.Domain.BO.SystemUser.Outputs;
using System_FishKoi.Domain.DAO.Client_User;
using System_FishKoi.Helper;

namespace System_FishKoi.Domain.BUS.Client_User
{
    public class Client_UserBUS : BaseBUS
    {
        private readonly Client_UserDAO _client_UserDAO = null;

        public Client_UserBUS()
        {
            _client_UserDAO = new Client_UserDAO();
        }

        public List<GetPagedListClient_User_Output> GetPagedList(string keySearch, int pageSize, int offset)
        {
            List<GetPagedListClient_User_Output> results = new List<GetPagedListClient_User_Output>();
            try
            {
                var dt = _client_UserDAO.GetPagedList(keySearch, pageSize, offset);
                if (dt != null && dt.Rows.Count > 0)
                {
                    results = DataTableHelper.DataTableToList<GetPagedListClient_User_Output>(dt);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }

            return results;
        }

        public GetSystemUser_Output SignIn(SignInSystemUser_Input bo)
        {
            GetSystemUser_Output result = null;
            try
            {
                var dt = _client_UserDAO.SignIn(bo);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = DataTableHelper.DataTableToObject<GetSystemUser_Output>(dt);
                }
                else
                {
                    _reponseMessage.Message = "Tài khoản hoặc mật khẩu không trùng khớp";
                    _reponseMessage.Status = MessageStatus.Warning;
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return result;
        }

        public int Register(RegisterClient_User_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Register(bo))
                {
                    returnVal = _client_UserDAO.Register(bo);
                    if (returnVal == -2)
                    {
                        _reponseMessage.Message = "Đã tồn tại mã thành viên này trong hệ thống";
                        _reponseMessage.Status = MessageStatus.Error;
                    }
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        public int UpdateStatus(UpdateStatusClient_User_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_UpdateStatus(bo))
                {
                    returnVal = _client_UserDAO.UpdateStatus(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        public int Delete(DeleteClient_User_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Delete(bo))
                {
                    returnVal = _client_UserDAO.Delete(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        private bool CheckIsValid_Register(RegisterClient_User_Input bo)
        {
            if (string.IsNullOrEmpty(bo.Username))
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập mã nhân viên";
                return false;
            }

            if (string.IsNullOrEmpty(bo.FullName))
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập tên nhân viên";
                return false;
            }
            return true;
        }

        private bool CheckIsValid_UpdateStatus(UpdateStatusClient_User_Input bo)
        {
            if (bo.ClientUserID == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy thông tin nhân viên này trong hệ thống";
                return false;
            }
            return true;
        }

        private bool CheckIsValid_Delete(DeleteClient_User_Input bo)
        {
            if (bo.ClientUserID == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy thông tin nhân viên này trong hệ thống";
                return false;
            }

            return true;
        }
    }
}