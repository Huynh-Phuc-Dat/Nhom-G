using System;
using System.Data;
using System_FishKoi.Domain.BO.Lake.Inputs;

namespace System_FishKoi.Domain.DAO.Lake
{
    public class Lake_SettingDAO : BaseDAO
    {
        private IData objData = null;
        public DataTable GetPagedList(string keySearch, int lakeID, int pageSize, int offset)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Lake_Setting_GetPagedList");
                objData.AddParameter("@KeySearch", keySearch);

                objData.AddParameter("@PageSize", pageSize);
                objData.AddParameter("@Offset", offset);
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

        public DataTable GetDetail(int settingID)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Lake_Setting_GetDetail");
                objData.AddParameter("@SettingID", settingID);

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

        public int Insert(InsertLake_Setting_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Lake_Setting_Insert");
                objData.AddParameter("@LakeID", bo.LakeID);

                if (bo.NO2 != 0) objData.AddParameter("@NO2", bo.NO2);
                if (bo.NO3 != 0) objData.AddParameter("@NO3", bo.NO3);
                objData.AddParameter("@Note", bo.Note);

                if (bo.O2 != 0) objData.AddParameter("@O2", bo.O2);
                if (bo.PH != 0) objData.AddParameter("@PH", bo.PH);
                if (bo.PO4 != 0) objData.AddParameter("@PO4", bo.PO4);

                if (bo.Salt != 0) objData.AddParameter("@Salt", bo.Salt);
                if (bo.Temperature != 0) objData.AddParameter("@Temperature", bo.Temperature);
                objData.AddParameter("@SettingDate", bo.SettingDate);

                objData.AddParameter("@UserLogin", _userLogin);
                objData.ExecNonQuery();
                returnVal = 1;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objData.Disconnect();
            }
            return returnVal;
        }

        public int Update(UpdateLake_Setting_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Lake_Setting_Update");
                objData.AddParameter("@LakeID", bo.LakeID);

                objData.AddParameter("@NO2", bo.NO2);
                objData.AddParameter("@NO3", bo.NO3);
                objData.AddParameter("@Note", bo.Note);

                objData.AddParameter("@O2", bo.O2);
                objData.AddParameter("@PH", bo.PH);
                objData.AddParameter("@PO4", bo.PO4);

                objData.AddParameter("@Salt", bo.Salt);
                objData.AddParameter("@SettingDate", bo.SettingDate);
                objData.AddParameter("@Temperature", bo.Temperature);

                objData.AddParameter("@UserLogin", _userLogin);
                objData.AddParameter("@SettingID", bo.SettingID);
                objData.ExecNonQuery();

                returnVal = 1;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objData.Disconnect();
            }
            return returnVal;
        }

        public int Delete(DeleteLake_Setting_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Lake_Setting_Delete");
                objData.AddParameter("@SettingID", bo.SettingID);
                objData.AddParameter("@UserLogin", _userLogin);

                objData.ExecNonQuery();
                returnVal = 1;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objData.Disconnect();
            }
            return returnVal;
        }
    }
}