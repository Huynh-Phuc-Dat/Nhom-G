using System;
using System.Data;
using System_FishKoi.Domain.BO.Product.Inputs;

namespace System_FishKoi.Domain.DAO.Product
{
    public class ProductDAO : BaseDAO
    {
        private IData objData = null;
        public DataTable GetPagedList(string keySearch, int categoryID, int pageSize, int offset)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Product_GetPagedList");
                objData.AddParameter("@KeySearch", keySearch);

                objData.AddParameter("@CategoryID", categoryID);
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

        public DataTable GetDetail(int fishKoiID)
        {
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Product_GetDetail");
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
                objData.CreateNewStoredProcedure("Product_GetAll");
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

        public int Insert(InsertProduct_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Product_Insert");
                objData.AddParameter("@CategoryID", bo.CategoryID);

                objData.AddParameter("@Description", bo.Description);
                objData.AddParameter    ("@Price", bo.Price);
                objData.AddParameter("@ProductCode", bo.ProductCode);

                objData.AddParameter("@ProductImage", bo.ProductImage);
                objData.AddParameter("@ProductName", bo.ProductName);
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

        public int Update(UpdateProduct_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Product_Update");
                objData.AddParameter("@CategoryID", bo.CategoryID);

                objData.AddParameter("@Description", bo.Description);
                objData.AddParameter("@Price", bo.Price);
                objData.AddParameter("@ProductCode", bo.ProductCode);

                objData.AddParameter("@ProductImage", bo.ProductImage);
                objData.AddParameter("@ProductName", bo.ProductName);
                objData.AddParameter("@ProductID", bo.ProductID);

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

        public int Delete(DeleteProduct_Input bo)
        {
            int returnVal = -1;
            objData = Data.CreateData();
            try
            {
                objData.Connect();
                objData.CreateNewStoredProcedure("Product_Delete");
                objData.AddParameter("@ProductID", bo.ProductID);
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