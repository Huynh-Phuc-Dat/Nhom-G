using System;
using System.Collections.Generic;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.SystemUser.Intputs;
using System_FishKoi.Domain.BO.SystemUser.Outputs;
using System_FishKoi.Domain.DAO.SystemUser;
using System_FishKoi.Helper;

namespace System_FishKoi.Domain.BUS.SystemUser
{
    public class SystemUserBUS : BaseBUS
    {
        private readonly SystemUserDAO _systemUserDAO = null;

        public SystemUserBUS()
        {
            _systemUserDAO = new SystemUserDAO();
        }

        public List<GetPagedListSystemUser_Output> GetPagedList(string keySearch, int pageSize, int offset)
        {
            List<GetPagedListSystemUser_Output> results = new List<GetPagedListSystemUser_Output>();
            try
            {
                var dt = _systemUserDAO.GetPagedList(keySearch, pageSize, offset);
                if (dt != null && dt.Rows.Count > 0)
                {
                    results = DataTableHelper.DataTableToList<GetPagedListSystemUser_Output>(dt);
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
                var dt = _systemUserDAO.SignIn(bo);
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

        public int Insert(InsertSystemUser_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Insert(bo))
                {
                    returnVal = _systemUserDAO.Insert(bo);
                    if (returnVal == -2)
                    {
                        _reponseMessage.Message = "Đã tồn tại mã nhân viên này trong hệ thống";
                        _reponseMessage.Status = MessageStatus.Warning;
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

        public int Update(UpdateSystemUser_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Update(bo))
                {
                    returnVal = _systemUserDAO.Update(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        public int UpdateStatus(UpdateStatusSystemUser_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_UpdateStatus(bo))
                {
                    returnVal = _systemUserDAO.UpdateStatus(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        public int Delete(DeleteSystemUser_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Delete(bo))
                {
                    returnVal = _systemUserDAO.Delete(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        private bool CheckIsValid_Insert(InsertSystemUser_Input bo)
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

        private bool CheckIsValid_Update(UpdateSystemUser_Input bo)
        {
            if (bo.UserID == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy thông tin nhân viên này trong hệ thống";
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

        private bool CheckIsValid_UpdateStatus(UpdateStatusSystemUser_Input bo)
        {
            if (bo.UserID == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy thông tin nhân viên này trong hệ thống";
                return false;
            }
            return true;
        }

        private bool CheckIsValid_Delete(DeleteSystemUser_Input bo)
        {
            if (bo.UserID == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy thông tin nhân viên này trong hệ thống";
                return false;
            }

            return true;
        }
    }
}