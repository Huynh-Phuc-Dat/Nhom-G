using System;
using System.Collections.Generic;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.Lake.Inputs;
using System_FishKoi.Domain.BO.Lake.Outputs;
using System_FishKoi.Domain.DAO.Lake;
using System_FishKoi.Helper;

namespace System_FishKoi.Domain.BUS.Lake
{
    public class Lake_SettingBUS : BaseBUS
    {
        private readonly Lake_SettingDAO _lake_SettingDAO = null;

        public Lake_SettingBUS()
        {
            _lake_SettingDAO = new Lake_SettingDAO();
        }

        public List<GetPagedListLake_Setting_Output> GetPagedList(string keySearch, int lakeID, int pageSize, int offset)
        {
            List<GetPagedListLake_Setting_Output> results = new List<GetPagedListLake_Setting_Output>();
            try
            {
                var dt = _lake_SettingDAO.GetPagedList(keySearch, lakeID, pageSize, offset);
                if (dt != null && dt.Rows.Count > 0)
                {
                    results = DataTableHelper.DataTableToList<GetPagedListLake_Setting_Output>(dt);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }

            return results;
        }

        public GetDetailLake_Setting_Output GetDetail(int settingID)
        {
            GetDetailLake_Setting_Output result = null;
            try
            {
                var dt = _lake_SettingDAO.GetDetail(settingID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = DataTableHelper.DataTableToObject<GetDetailLake_Setting_Output>(dt);
                }
                else
                {
                    _reponseMessage.Message = "Không tìm thấy dữ liệu thông số nước này trong hệ thống";
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

        public int Insert(InsertLake_Setting_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Insert(bo))
                {
                    returnVal = _lake_SettingDAO.Insert(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        public int Update(UpdateLake_Setting_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Update(bo))
                {
                    returnVal = _lake_SettingDAO.Update(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        public int Delete(DeleteLake_Setting_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Delete(bo))
                {
                    returnVal = _lake_SettingDAO.Delete(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        private bool CheckIsValid_Insert(InsertLake_Setting_Input bo)
        {
            if (bo.LakeID < 1)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng chọn hồ cá";
                return false;
            }
            if (bo.SettingDate == DateTime.MinValue)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng chọn ngày thiết lập thông số nước";
                return false;
            }
            return true;
        }

        private bool CheckIsValid_Update(UpdateLake_Setting_Input bo)
        {
            if (bo.SettingID == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy thông tin này trong hệ thống";
                return false;
            }

            if (bo.LakeID < 1)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng chọn hồ cá";
                return false;
            }
            if (bo.SettingDate == DateTime.MinValue)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng chọn ngày thiết lập thông số nước";
                return false;
            }
            return true;
        }

        private bool CheckIsValid_Delete(DeleteLake_Setting_Input bo)
        {
            if (bo.SettingID == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy thông tin này trong hệ thống";
                return false;
            }

            return true;
        }
    }
}