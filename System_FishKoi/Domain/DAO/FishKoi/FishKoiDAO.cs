using System;
using System.Data;
using System_FishKoi.Domain.BO.FishKoi.Inputs;

namespace System_FishKoi.Domain.DAO.FishKoi
{
    public class FishKoiDAO : BaseDAO
    {
        private IData objData = null;
        public DataTable GetPagedList(string keySearch, int fishKoiGender, int pageSize, int offset, int status)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("FishKoi_GetPagedList");
                objData.AddParameter("@KeySearch", keySearch);

                objData.AddParameter("@FishKoiGender", fishKoiGender);
                objData.AddParameter("@PageSize", pageSize);
                objData.AddParameter("@Offset", offset);

                objData.AddParameter("@Status", status);
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

        public DataTable GetDetail(int fishKoiID)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("FishKoi_GetDetail");
                objData.AddParameter("@FishKoiID", fishKoiID);

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
                objData.CreateNewStoredProcedure("FishKoi_GetAll");
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

        public int Insert(InsertFishKoi_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("FishKoi_Insert");
                objData.AddParameter("@FishKoiAge", bo.FishKoiAge);

                objData.AddParameter("@FishKoiFace", bo.FishKoiFace);
                objData.AddParameter("@FishKoiGender", bo.FishKoiGender);
                objData.AddParameter("@FishKoiImage", bo.FishKoiImage);

                objData.AddParameter("@FishKoiName", bo.FishKoiName);
                if (bo.FishKoiPrice != 0) objData.AddParameter("@FishKoiPrice", bo.FishKoiPrice);
                objData.AddParameter("@FishKoiSource", bo.FishKoiSource);

                if (bo.FishKoiWeight != 0) objData.AddParameter("@FishKoiWeight", bo.FishKoiWeight);
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

        public int Update(UpdateFishKoi_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("FishKoi_Update");
                objData.AddParameter("@FishKoiAge", bo.FishKoiAge);

                objData.AddParameter("@FishKoiFace", bo.FishKoiFace);
                objData.AddParameter("@FishKoiGender", bo.FishKoiGender);
                objData.AddParameter("@FishKoiImage", bo.FishKoiImage);

                objData.AddParameter("@FishKoiName", bo.FishKoiName);
                if (bo.FishKoiPrice != 0) objData.AddParameter("@FishKoiPrice", bo.FishKoiPrice);
                objData.AddParameter("@FishKoiSource", bo.FishKoiSource);

                if (bo.FishKoiWeight != 0) objData.AddParameter("@FishKoiWeight", bo.FishKoiWeight);
                objData.AddParameter("@FishKoiID", bo.FishKoiID);
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

        public int Delete(DeleteFishKoi_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("FishKoi_Delete");
                objData.AddParameter("@FishKoiID", bo.FishKoiID);
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