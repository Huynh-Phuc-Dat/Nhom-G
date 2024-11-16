using System;
using System.Collections.Generic;
using System.Web;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.FishKoi.Inputs;
using System_FishKoi.Domain.BO.FishKoi.Outputs;
using System_FishKoi.Domain.DAO.FishKoi;
using System_FishKoi.Helper;

namespace System_FishKoi.Domain.BUS.FishKoi
{
    public class FishKoiBUS : BaseBUS
    {
        private readonly FishKoiDAO _fishKoiDAO = null;

        public FishKoiBUS()
        {
            _fishKoiDAO = new FishKoiDAO();
        }

        public List<GetPagedListFishKoi_Output> GetPagedList(string keySearch, int fishKoiGender, int pageSize, int offset, int status)
        {
            List<GetPagedListFishKoi_Output> results = new List<GetPagedListFishKoi_Output>();
            try
            {
                var dt = _fishKoiDAO.GetPagedList(keySearch, fishKoiGender, pageSize, offset, status);
                if (dt != null && dt.Rows.Count > 0)
                {
                    results = DataTableHelper.DataTableToList<GetPagedListFishKoi_Output>(dt);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }

            return results;
        }

        public List<GetAllFishKoi_Output> GetAll()
        {
            List<GetAllFishKoi_Output> results = new List<GetAllFishKoi_Output>();
            try
            {
                var dt = _fishKoiDAO.GetAll();
                if (dt != null && dt.Rows.Count > 0)
                {
                    results = DataTableHelper.DataTableToList<GetAllFishKoi_Output>(dt);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }

            return results;
        }

        public GetDetailFishKoi_Output GetDetail(int fishKoiID)
        {
            GetDetailFishKoi_Output result = null;
            try
            {
                var dt = _fishKoiDAO.GetDetail(fishKoiID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string baseUrl = $"{HttpContext.Current.Request.Url.Scheme}://{HttpContext.Current.Request.Url.Authority}/";
                    result = DataTableHelper.DataTableToObject<GetDetailFishKoi_Output>(dt);
                    result.FishKoiLink = baseUrl + result.FishKoiImage;
                }
                else
                {
                    _reponseMessage.Message = "Không tìm thấy dữ liệu cá Koi này trong hệ thống";
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

        public int Insert(InsertFishKoi_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Insert(bo))
                {
                    returnVal = _fishKoiDAO.Insert(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        public int Update(UpdateFishKoi_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Update(bo))
                {
                    returnVal = _fishKoiDAO.Update(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        public int Delete(DeleteFishKoi_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Delete(bo))
                {
                    returnVal = _fishKoiDAO.Delete(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        private bool CheckIsValid_Insert(InsertFishKoi_Input bo)
        {
            if (string.IsNullOrEmpty(bo.FishKoiName))
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập tên cá Koi";
                return false;
            }

            if (bo.FishKoiAge == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập tuổi";
                return false;
            }

            if (bo.FishKoiGender == -1)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng chọn giới tính";
                return false;
            }

            return true;
        }

        private bool CheckIsValid_Update(UpdateFishKoi_Input bo)
        {
            if (bo.FishKoiID == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy thông tin cá Koi này trong hệ thống";
                return false;
            }

            if (string.IsNullOrEmpty(bo.FishKoiName))
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập tên cá Koi";
                return false;
            }

            if (bo.FishKoiAge == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập tuổi";
                return false;
            }

            if (bo.FishKoiGender == -1)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng chọn giới tính";
                return false;
            }

            return true;
        }

        private bool CheckIsValid_Delete(DeleteFishKoi_Input bo)
        {
            if (bo.FishKoiID == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy thông tin cá Koi này trong hệ thống";
                return false;
            }

            return true;
        }
    }
}