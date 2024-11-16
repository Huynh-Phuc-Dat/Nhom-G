using System;
using System.Data;
using System_FishKoi.Domain.BO.SystemUser.Intputs;

namespace System_FishKoi.Domain.DAO.SystemUser
{
    public class SystemUserDAO : BaseDAO
    {
        private IData objData = null;
        public DataTable GetPagedList(string keySearch, int pageSize, int offset)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("System_User_GetPagedList");
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

        public DataTable SignIn(SignInSystemUser_Input bo)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("System_User_SignIn");
                objData.AddParameter("@Username", bo.Username);

                objData.AddParameter("@Password", bo.Password);
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

        public int Insert(InsertSystemUser_Input bo)
        {
            objData = Data.CreateData();
            int returnVal = -1;
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("System_User_Insert");
                objData.AddParameter("@Email", bo.Email);

                objData.AddParameter("@FullName", bo.FullName);
                objData.AddParameter("@Username", bo.Username);
                objData.AddParameter("@Password", bo.Password);

                objData.AddParameter("@UserLogin", _userLogin);
                returnVal = Convert.ToInt32(objData.ExecStoreToString());
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

        public int Update(UpdateSystemUser_Input bo)
        {
            objData = Data.CreateData();
            int returnVal = -1;
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("System_User_Update");
                objData.AddParameter("@Email", bo.Email);

                objData.AddParameter("@FullName", bo.FullName);
                objData.AddParameter("@Password", bo.Password);
                objData.AddParameter("@UserLogin", _userLogin);

                objData.AddParameter("@UserID", bo.UserID);
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

        public int Delete(DeleteSystemUser_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("System_User_Delete");
                objData.AddParameter("@UserID", bo.UserID);

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

        public int UpdateStatus(UpdateStatusSystemUser_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("System_User_UpdateStatus");
                objData.AddParameter("@UserID", bo.UserID);

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