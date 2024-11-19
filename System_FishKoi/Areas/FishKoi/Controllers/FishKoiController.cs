using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System_FishKoi.Controllers;
using System_FishKoi.Domain.BO.Common.Outputs;
using System_FishKoi.Domain.BO.FishKoi.Inputs;
using System_FishKoi.Domain.BUS.FishKoi;

namespace System_FishKoi.Areas.FishKoi
{
    public class FishKoiController : BaseController
    {
        private string ATTACH_DIRECTORY = "Upload/FishKoi";
        private readonly FishKoiBUS _fishKoiBUS = null;
        public FishKoiController()
        {
            _fishKoiBUS = new FishKoiBUS();
        }
            
        public ActionResult FishKoi()
        {
            return View();
        }

        public ActionResult FishKoi_Insert()
        {
            return View();
        }

        public ActionResult FishKoi_Update()
        {
            return View();
        }

        public JsonResult GetPagedList(string keySearch, int draw, int fishKoiGender, int pageSize, int offset, int status)
        {
            var results = _fishKoiBUS.GetPagedList(keySearch, fishKoiGender, pageSize, offset, status);
            _reponseMessage = _fishKoiBUS.GetReponseMessage();
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
        public JsonResult GetDetail(int fishKoiID)
        {
            var result = _fishKoiBUS.GetDetail(fishKoiID);
            _reponseMessage = _fishKoiBUS.GetReponseMessage();
            _reponseMessage.Data = result;
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _fishKoiBUS.GetAll();
            _reponseMessage = _fishKoiBUS.GetReponseMessage();
            _reponseMessage.Data = result;
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insert(InsertFishKoi_Input bo)
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
                bo.FishKoiImage = ATTACH_DIRECTORY + "/" + STR_MONTH_YEAR + "/" + postedFile.FileName;

                result = _fishKoiBUS.Insert(bo);
                _reponseMessage = _fishKoiBUS.GetReponseMessage();
            }
            else
            {
                _reponseMessage.Status = MessageStatus.Warning;
                _reponseMessage.Message = "Vui lòng up load hình ảnh cá Koi";
            }

            _reponseMessage.Data = result;
            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(UpdateFishKoi_Input bo)
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
                bo.FishKoiImage = ATTACH_DIRECTORY + "/" + STR_MONTH_YEAR + "/" + postedFile.FileName;
            }

            int result = _fishKoiBUS.Update(bo);
            _reponseMessage = _fishKoiBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(DeleteFishKoi_Input bo)
        {
            int result = _fishKoiBUS.Delete(bo);
            _reponseMessage = _fishKoiBUS.GetReponseMessage();
            _reponseMessage.Data = result;

            return Json(_reponseMessage, JsonRequestBehavior.AllowGet);
        }
    }
}