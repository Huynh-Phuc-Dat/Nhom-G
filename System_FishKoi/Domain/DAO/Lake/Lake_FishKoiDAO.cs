using System;
using System.Data;
using System_FishKoi.Domain.BO.Lake.Inputs;

namespace System_FishKoi.Domain.DAO.Lake
{
    public class Lake_FishKoiDAO : BaseDAO
    {
        private IData objData = null;
        public DataTable GetList(int lakeID)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Lake_FishKoi_GetList");
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

        public int Insert(InsertLake_FishKoi_Input bo, IData objData = null)
        {
            int returnVal = -1;
            try
            {
                objData.CreateNewStoredProcedure("Lake_FishKoi_Insert");
                objData.AddParameter("@FishKoiID", bo.FishKoiID);

                objData.AddParameter("@LakeID", bo.LakeID);
                objData.AddParameter("@Quantity", bo.Quantity);
                objData.AddParameter("@UserLogin", _userLogin);

                objData.ExecNonQuery();
                returnVal = 1;
            }
            catch (Exception)
            {
                throw;
            }
            return returnVal;
        }

        public int Delete(DeleteLake_FishKoi_Input bo, IData objData = null)
        {
            int returnVal = -1;
            try
            {
                objData.CreateNewStoredProcedure("Lake_FishKoi_Delete");
                objData.AddParameter("@LakeID", bo.LakeID);

                objData.AddParameter("@UserLogin", _userLogin);
                objData.ExecNonQuery();
                returnVal = 1;
            }
            catch (Exception)
            {
                throw;
            }
            return returnVal;
        }
    }
}