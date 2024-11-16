namespace System_FishKoi.Domain.BO.FishKoi.Outputs
{
    public class GetPagedListFishKoi_Output
    {
        public int FishKoiID { get; set; }
        public string FishKoiName { get; set; }
        public string FishKoiGenderName { get; set; }
        public int FishKoiGender { get; set; }
        public int FishKoiAge { get; set; }
        public float? FishKoiWeight { get; set; }
        public string FishKoiFace { get; set; }
        public string FishKoiSource { get; set; }
        public float? FishKoiPrice { get; set; }
        public int TotalRow { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedUser { get; set; }
    }
}