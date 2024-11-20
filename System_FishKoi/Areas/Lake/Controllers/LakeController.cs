using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System_FishKoi.Controllers;
using System_FishKoi.Domain.BO.Common;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.Lake.Inputs;
using System_FishKoi.Domain.BUS.Lake;

namespace System_FishKoi.Areas.Lake
{
    public class LakeController : BaseController
    {
        private readonly LakeBUS _lakeBUS = null;
        public LakeController()
        {
            _lakeBUS = new LakeBUS();
        }

        public ActionResult Lake()
        {
            return View();
        }

        public ActionResult Lake_Insert()
        {
            return View();
        }

        public ActionResult Lake_Update()
        {
            return View();
        }

        public JsonResult UploadFile(UploadFile bo)
        {
            HttpPostedFileBase postedFile = Request.Files["file"];
            if (postedFile == null)
            {
                _reponseMessage.Message = "Vui lòng upload file";
                _reponseMessage.Status = MessageStatus.Warning;
            }
            else
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + bo.Directory + "/" + STR_MONTH_YEAR;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(path + "/" + postedFile.FileName, FileMode.Create, FileAccess.Write))
                {
                    postedFile.InputStream.CopyTo(fileStream);
                }
                _reponseMessage.Data = bo.Directory + "/" + STR_MONTH_YEAR + "/" + postedFile.FileName;
            }
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPagedList(string keySearch, int draw, int pageSize, int offset)
        {
            var results = _lakeBUS.GetPagedList(keySearch, pageSize, offset);
            _reponseMessage = _lakeBUS.GetReponseMessage();
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
        public JsonResult GetDetail(int lakeID)
        {
            var result = _lakeBUS.GetDetail(lakeID);
            _reponseMessage = _lakeBUS.GetReponseMessage();
            _reponseMessage.Data = result;
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _lakeBUS.GetAll();
            _reponseMessage = _lakeBUS.GetReponseMessage();
            _reponseMessage.Data = result;
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insert(InsertLake_Input bo)
        {
            int result = _lakeBUS.Insert(bo);
            _reponseMessage = _lakeBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(UpdateLake_Input bo)
        {
            int result = _lakeBUS.Update(bo);
            _reponseMessage = _lakeBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(DeleteLake_Input bo)
        {
            int result = _lakeBUS.Delete(bo);
            _reponseMessage = _lakeBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }
    }
}