using System_FishKoi.Domain.BO.Common;

namespace System_FishKoi.Domain.BO.FishKoi.Inputs
{
    public class InsertFishKoi_Input : UploadFile
    {
        public string FishKoiName { get; set; }
        public int FishKoiAge { get; set; }
        public float FishKoiWeight { get; set; }
        public int FishKoiGender { get; set; }
        public string FishKoiFace { get; set; }
        public string FishKoiSource { get; set; }
        public float FishKoiPrice { get; set; }
        public string FishKoiImage { get; set; }
    }
}