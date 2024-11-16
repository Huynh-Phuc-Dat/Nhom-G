using System;

namespace System_FishKoi.Domain.BO.Lake.Outputs
{
    public class GetPagedListLake_Setting_Output
    {
        public int LakeID { get; set; }
        public string LakeName { get; set; }
        public int SettingID { get; set; }
        public DateTime SettingDate { get; set; }
        public float? Temperature { get; set; }
        public float? Salt { get; set; }
        public float? PH { get; set; }
        public float? O2 { get; set; }
        public float? NO2 { get; set; }
        public float? NO3 { get; set; }
        public float? PO4 { get; set; }
        public string Note { get; set; }
        public int TotalRow { get; set; }
        public string CreatedUser { get; set; }
    }
}