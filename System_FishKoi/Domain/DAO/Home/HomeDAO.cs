using System;
using System.Data;

namespace System_FishKoi.Domain.DAO.Home
{
    public class HomeDAO : BaseDAO
    {
        private IData objData = null;
        public DataTable GetData_Total_System_FishKoi()
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("GetData_Total_System_FishKoi");
                return objData.ExecStoreToDataTable();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objData.Disconnect();
            }
        }

        public DataTable GetData_AVG_Lake_Setting(int lakeID)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("GetData_AVG_Lake_Setting");
                objData.AddParameter("@LakeID", lakeID);

                return objData.ExecStoreToDataTable();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objData.Disconnect();
            }
        }

        public DataTable GetData_Lake_Total_FishKoi()
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("GetData_Lake_Total_FishKoi");
                return objData.ExecStoreToDataTable();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objData.Disconnect();
            }
        }
    }
}