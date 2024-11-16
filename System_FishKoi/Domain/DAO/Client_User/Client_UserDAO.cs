using System;
using System.Data;
using System_FishKoi.Domain.BO.Client_User.Intputs;
using System_FishKoi.Domain.BO.SystemUser.Intputs;

namespace System_FishKoi.Domain.DAO.Client_User
{
    public class Client_UserDAO : BaseDAO
    {
        private IData objData = null;
        public DataTable GetPagedList(string keySearch, int pageSize, int offset)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Client_User_GetPagedList");
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
                objData.CreateNewStoredProcedure("Client_User_SignIn");
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

        public int Register(RegisterClient_User_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Client_User_Insert");
                objData.AddParameter("@Username", bo.Username);

                objData.AddParameter("@Password", bo.Password);
                objData.AddParameter("@FullName", bo.FullName);
                objData.AddParameter("@Email", bo.Email);

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

        public int Delete(DeleteClient_User_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Client_User_Delete");
                objData.AddParameter("@ClientUserID", bo.ClientUserID);

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

        public int UpdateStatus(UpdateStatusClient_User_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Client_User_UpdateStatus");
                objData.AddParameter("@ClientUserID", bo.ClientUserID);

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