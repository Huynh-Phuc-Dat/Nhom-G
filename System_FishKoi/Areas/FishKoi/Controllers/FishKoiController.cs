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

        
    }
}