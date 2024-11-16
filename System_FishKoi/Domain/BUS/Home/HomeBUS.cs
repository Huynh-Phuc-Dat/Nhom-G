using System;
using System.Collections.Generic;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.Home.Outputs;
using System_FishKoi.Domain.DAO.Home;
using System_FishKoi.Helper;

namespace System_FishKoi.Domain.BUS.Home
{
    public class HomeBUS : BaseBUS
    {
        private readonly HomeDAO _homeDAO = null;

        public HomeBUS()
        {
            _homeDAO = new HomeDAO();
        }

        public GetData_Total_System_FishKoiOutput GetData_Total_System_FishKoi()
        {
            GetData_Total_System_FishKoiOutput result = null;
            try
            {
                var dt = _homeDAO.GetData_Total_System_FishKoi();
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = DataTableHelper.DataTableToObject<GetData_Total_System_FishKoiOutput>(dt);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }

            return result;
        }

        public List<GetData_AVG_Lake_SettingOutput> GetData_AVG_Lake_Setting(int lakeID)
        {
            List<GetData_AVG_Lake_SettingOutput> results = new List<GetData_AVG_Lake_SettingOutput>();
            try
            {
                var dt = _homeDAO.GetData_AVG_Lake_Setting(lakeID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    results = DataTableHelper.DataTableToList<GetData_AVG_Lake_SettingOutput>(dt);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }

            return results;
        }

        public List<GetData_Lake_Total_FishKoiOutput> GetData_Lake_Total_FishKoi()
        {
            List<GetData_Lake_Total_FishKoiOutput> results = new List<GetData_Lake_Total_FishKoiOutput>();
            try
            {
                var dt = _homeDAO.GetData_Lake_Total_FishKoi();
                if (dt != null && dt.Rows.Count > 0)
                {
                    results = DataTableHelper.DataTableToList<GetData_Lake_Total_FishKoiOutput>(dt);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return results;
        }
    }
}