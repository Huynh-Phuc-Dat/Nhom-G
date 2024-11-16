using System;
using System.Collections.Generic;
using System.Web;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.Product.Inputs;
using System_FishKoi.Domain.BO.Product.Outputs;
using System_FishKoi.Domain.DAO.Product;
using System_FishKoi.Helper;

namespace System_FishKoi.Domain.BUS.Product
{
    public class ProductBUS : BaseBUS
    {
        private readonly ProductDAO _productDAO = null;

        public ProductBUS()
        {
            _productDAO = new ProductDAO();
        }

        public List<GetPagedListProduct_Output> GetPagedList(string keySearch, int categoryID, int pageSize, int offset)
        {
            List<GetPagedListProduct_Output> results = new List<GetPagedListProduct_Output>();
            try
            {
                var dt = _productDAO.GetPagedList(keySearch, categoryID, pageSize, offset);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string baseUrl = $"{HttpContext.Current.Request.Url.Scheme}://{HttpContext.Current.Request.Url.Authority}/";
                    results = DataTableHelper.DataTableToList<GetPagedListProduct_Output>(dt);
                    foreach (var item in results)
                    {
                        item.ProductLink = baseUrl + item.ProductImage;
                    }
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }

            return results;
        }

        public List<GetAllProduct_Output> GetAll()
        {
            List<GetAllProduct_Output> results = new List<GetAllProduct_Output>();
            try
            {
                var dt = _productDAO.GetAll();
                if (dt != null && dt.Rows.Count > 0)
                {
                    results = DataTableHelper.DataTableToList<GetAllProduct_Output>(dt);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }

            return results;
        }

        public int Insert(InsertProduct_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Insert(bo))
                {
                    returnVal = _productDAO.Insert(bo);
                    if (returnVal == -2)
                    {
                        _reponseMessage.Status = MessageStatus.Warning;
                        _reponseMessage.Message = "Mã sản phẩm này đã tồn tại trong hệ thống hoặc đã bị xoá";
                    }
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        public int Update(UpdateProduct_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Update(bo))
                {
                    returnVal = _productDAO.Update(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        public int Delete(DeleteProduct_Input bo)
        {
            int returnVal = -1;
            try
            {
                if (CheckIsValid_Delete(bo))
                {
                    returnVal = _productDAO.Delete(bo);
                }
            }
            catch (Exception objEx)
            {
                _reponseMessage.Message = objEx.Message;
                _reponseMessage.Status = MessageStatus.Error;
            }
            return returnVal;
        }

        private bool CheckIsValid_Insert(InsertProduct_Input bo)
        {
            if (string.IsNullOrEmpty(bo.ProductCode))
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập mã sản phẩm";
                return false;
            }

            if (string.IsNullOrEmpty(bo.ProductName))
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập tên sản phẩm";
                return false;
            }

            if (bo.CategoryID < 1)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng chọn danh mục sản phẩm";
                return false;
            }

            return true;
        }

        private bool CheckIsValid_Update(UpdateProduct_Input bo)
        {
            if (bo.ProductID < 1)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy mã sản phẩm này trong hệ thống";
                return false;
            }

            if (string.IsNullOrEmpty(bo.ProductCode))
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập mã sản phẩm";
                return false;
            }

            if (string.IsNullOrEmpty(bo.ProductName))
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng nhập tên sản phẩm";
                return false;
            }

            if (bo.CategoryID < 1)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng chọn danh mục sản phẩm";
                return false;
            }

            return true;
        }

        private bool CheckIsValid_Delete(DeleteProduct_Input bo)
        {
            if (bo.ProductID < 1)
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Không tìm thấy mã sản phẩm này trong hệ thống";
                return false;
            }

            return true;
        }
    }
}