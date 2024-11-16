using System;
using System.Collections.Generic;
using System.Web;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.Lake.Inputs;
using System_FishKoi.Domain.BO.Lake.Outputs;
using System_FishKoi.Domain.DAO;
using System_FishKoi.Domain.DAO.Lake;
using System_FishKoi.Helper;

namespace System_FishKoi.Domain.BUS.Lake
{
    public class LakeBUS : BaseBUS
    {
        private readonly LakeDAO _lakeDAO = null;
        private readonly Lake_FishKoiDAO _lakeFishKoiDAO = null;

        public LakeBUS()
        {
            _lakeDAO = new LakeDAO();
            _lakeFishKoiDAO = new Lake_FishKoiDAO();
        }

        public List<GetPagedListLake_Output> GetPagedList(string keySearch, int pageSize, int offset)
        {
            List<GetPagedListLake_Output> results = new List<GetPagedListLake_Output>();
            try
            {
                var dt = _lakeDAO.GetPagedList(keySearch, pageSize, offset);
                if (dt != null && dt.Rows.Count > 0)
                {
                    results = DataTableHelper.DataTableToList<GetPagedListLake_Output>(dt);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }

            return results;
        }

        public List<GetAllLake_Output> GetAll()
        {
            List<GetAllLake_Output> results = new List<GetAllLake_Output>();
            try
            {
                var dt = _lakeDAO.GetAll();
                if (dt != null && dt.Rows.Count > 0)
                {
                    results = DataTableHelper.DataTableToList<GetAllLake_Output>(dt);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }

            return results;
        }

        public GetDetailLake_Output GetDetail(int lakeID)
        {
            GetDetailLake_Output result = null;
            try
            {
                var dt = _lakeDAO.GetDetail(lakeID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = DataTableHelper.DataTableToObject<GetDetailLake_Output>(dt);
                    result.LakeLink = $"{HttpContext.Current.Request.Url.Scheme}://{HttpContext.Current.Request.Url.Authority}/"; ;
                    var dt1 = _lakeFishKoiDAO.GetList(lakeID);
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        result.items = DataTableHelper.DataTableToList<GetListLake_FishKoi_Output>(dt1);
                    }
                }
                else
                {
                    _reponseMessage.Message = "Không tìm thấy dữ liệu hồ cá này trong hệ thống";
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

        public int Insert(InsertLake_Input bo)
        {
            int returnVal = -1;
            if (CheckIsValid_Insert(bo))
            {
                IData objData = Data.CreateData();
                try
                {
                    objData.Connect();
                    objData.BeginTransaction();
                    returnVal = _lakeDAO.Insert(bo, objData);

                    if (bo.items != null && bo.items.Count > 0)
                    {
                        foreach (var item in bo.items)
                        {
                            item.LakeID = returnVal;
                            _lakeFishKoiDAO.Insert(item, objData);
                        }
                    }
                    objData.Commit();

                }
                catch (Exception objEx)
                {
                    _reponseMessage.Message = objEx.Message;
                    _reponseMessage.Status = MessageStatus.Error;
                    returnVal = -1;
                }
                finally
                {
                    objData.Disconnect();
                }
            }
            return returnVal;
        }

        public int Update(UpdateLake_Input bo)
        {
            int returnVal = -1;
            if (CheckIsValid_Update(bo))
            {
                IData objData = Data.CreateData();
                try
                {
                    objData.Connect();
                    objData.BeginTransaction();
                    _lakeDAO.Update(bo, objData);

                    _lakeFishKoiDAO.Delete(new DeleteLake_FishKoi_Input() { LakeID = bo.LakeID }, objData);
                    if (bo.items != null && bo.items.Count > 0)
                    {
                        foreach (var item in bo.items)
                        {
                            item.LakeID = bo.LakeID;
                            _lakeFishKoiDAO.Insert(item, objData);
                        }
                    }
                    objData.Commit();
                    returnVal = 1;
                }
                catch (Exception objEx)
                {
                    _reponseMessage.Message = objEx.Message;
                    _reponseMessage.Status = MessageStatus.Error;
                }
                finally
                {
                    objData.Disconnect();
                }
            }
            return returnVal;
        }

        public int Delete(DeleteLake_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Delete(bo))
                {
                    returnVal = _lakeDAO.Delete(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        private bool CheckIsValid_Insert(InsertLake_Input bo)
        {
            if (string.IsNullOrEmpty(bo.LakeName))
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập tên hồ cá";
                return false;
            }
            return true;
        }

        private bool CheckIsValid_Update(UpdateLake_Input bo)
        {
            if (bo.LakeID == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy thông tin hồ cá này trong hệ thống";
                return false;
            }

            if (string.IsNullOrEmpty(bo.LakeName))
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập tên hồ cá";
                return false;
            }

            return true;
        }

        private bool CheckIsValid_Delete(DeleteLake_Input bo)
        {
            if (bo.LakeID == 0)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy thông tin hồ cá này trong hệ thống";
                return false;
            }

            return true;
        }
    }
}