using System;

namespace System_FishKoi.Domain.BO.Lake.Inputs
{
    public class UpdateLake_Setting_Input
    {
        public int SettingID { get; set; }
        public int LakeID { get; set; }
        public DateTime SettingDate { get; set; }
        public float? Temperature { get; set; }
        public float? Salt { get; set; }
        public float? PH { get; set; }
        public float? O2 { get; set; }
        public float? NO2 { get; set; }
        public float? NO3 { get; set; }
        public float? PO4 { get; set; }
        public string Note { get; set; }
    }
}