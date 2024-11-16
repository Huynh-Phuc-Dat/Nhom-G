using System;
using System.Data;
using System_FishKoi.Domain.BO.Lake.Inputs;

namespace System_FishKoi.Domain.DAO.Lake
{
    public class LakeDAO : BaseDAO
    {
        private IData objData = null;
        public DataTable GetPagedList(string keySearch, int pageSize, int offset)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Lake_GetPagedList");
                objData.AddParameter("@KeySearch", keySearch);

                objData.AddParameter("@PageSize", pageSize);
                objData.AddParameter("@Offset", offset);

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

        public DataTable GetDetail(int lakeID)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Lake_GetDetail");
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

        public DataTable GetAll()
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Lake_GetAll");
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

        public int Insert(InsertLake_Input bo, IData objData)
        {
            int returnVal = -1;
            try
            {
                objData.CreateNewStoredProcedure("Lake_Insert");
                if (bo.Depth != 0) objData.AddParameter("@Depth", bo.Depth);

                if (bo.PumpOutput != 0) objData.AddParameter("@PumpOutput", bo.PumpOutput);
                if (bo.QuantityDrain != 0) objData.AddParameter("@QuantityDrain", bo.QuantityDrain);
                if (bo.Volume != 0) objData.AddParameter("@Volume", bo.Volume);

                if (bo.Size != 0) objData.AddParameter("@Size", bo.Size);
                objData.AddParameter("@LakeImage", bo.LakeImage);
                objData.AddParameter("@LakeName", bo.LakeName);

                objData.AddParameter("@UserLogin", _userLogin);
                returnVal = Convert.ToInt32(objData.ExecStoreToString());
            }
            catch (Exception)
            {
                throw;
            }
            return returnVal;
        }

        public int Update(UpdateLake_Input bo, IData objData)
        {
            int returnVal = -1;
            try
            {
                objData.CreateNewStoredProcedure("Lake_Update");
                if (bo.Depth != 0) objData.AddParameter("@Depth", bo.Depth);

                if (bo.PumpOutput != 0) objData.AddParameter("@PumpOutput", bo.PumpOutput);
                if (bo.QuantityDrain != 0) objData.AddParameter("@QuantityDrain", bo.QuantityDrain);
                if (bo.Volume != 0) objData.AddParameter("@Volume", bo.Volume);

                if (bo.Size != 0) objData.AddParameter("@Size", bo.Size);
                objData.AddParameter("@LakeImage", bo.LakeImage);
                objData.AddParameter("@LakeName", bo.LakeName);

                objData.AddParameter("@UserLogin", _userLogin);
                objData.AddParameter("@LakeID", bo.LakeID);
                objData.ExecNonQuery();

                returnVal = 1;
            }
            catch (Exception)
            {
                throw;
            }
            return returnVal;
        }

        public int Delete(DeleteLake_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Lake_Delete");
                objData.AddParameter("@LakeID", bo.LakeID);
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