using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System_FishKoi.Controllers;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.Product.Inputs;
using System_FishKoi.Domain.BUS.Product;

namespace System_FishKoi.Areas.Product
{
    [AuthorizeByRole]
    public class ProductController : BaseController
    {
        private string ATTACH_DIRECTORY = "Upload/Product";
        private readonly ProductBUS _productBUS = null;
        public ProductController()
        {
            _productBUS = new ProductBUS();
        }

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult Product_Insert()
        {
            return View();
        }

        public ActionResult Product_Update()
        {
            return View();
        }

        public JsonResult GetPagedList(string keySearch, int draw, int categoryID, int pageSize, int offset)
        {
            var results = _productBUS.GetPagedList(keySearch, categoryID, pageSize, offset);
            _reponseMessage = _productBUS.GetReponseMessage();
            int recordsTotal = 0;

            int recordsFiltered = 0;
            if (_reponseMessage.Status == MessageStatus.Success)
            {
                recordsTotal = results.Count > 0 ? results[0].TotalRow : 0;
                recordsFiltered = results.Count > 0 ? results[0].TotalRow : 0;
            }

            _reponseMessage.Data = new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = results,
            };
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _productBUS.GetAll();
            _reponseMessage = _productBUS.GetReponseMessage();
            _reponseMessage.Data = result;
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insert(InsertProduct_Input bo)
        {
            int result = -1;
            HttpPostedFileBase postedFile = Request.Files["value_file"];
            if (postedFile != null)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + ATTACH_DIRECTORY + "/" + STR_MONTH_YEAR;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(path + "/" + postedFile.FileName, FileMode.Create, FileAccess.Write))
                {
                    postedFile.InputStream.CopyTo(fileStream);
                }
                bo.ProductImage = ATTACH_DIRECTORY + "/" + STR_MONTH_YEAR + "/" + postedFile.FileName;

                result = _productBUS.Insert(bo);
                _reponseMessage = _productBUS.GetReponseMessage();
            }
            else
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng up load hình ảnh sản phẩm";
            }

            _reponseMessage.Data = result;
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(UpdateProduct_Input bo)
        {
            HttpPostedFileBase postedFile = Request.Files["value_file"];
            if (postedFile != null)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + ATTACH_DIRECTORY + "/" + STR_MONTH_YEAR;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(path + "/" + postedFile.FileName, FileMode.Create, FileAccess.Write))
                {
                    postedFile.InputStream.CopyTo(fileStream);
                }
                bo.ProductImage = ATTACH_DIRECTORY + "/" + STR_MONTH_YEAR + "/" + postedFile.FileName;
            }

            int result = _productBUS.Update(bo);
            _reponseMessage = _productBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(DeleteProduct_Input bo)
        {
            int result = _productBUS.Delete(bo);
            _reponseMessage = _productBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }
    }
}