using System.Collections.Generic;

namespace System_FishKoi.Domain.BO.Lake.Inputs
{
    public class UpdateLake_Input
    {
        public int LakeID { get; set; }
        public string LakeName { get; set; }
        public float? Depth { get; set; }
        public int? Volume { get; set; }
        public int? QuantityDrain { get; set; }
        public int? PumpOutput { get; set; }
        public int? Size { get; set; }
        public string LakeImage { get; set; }
        public List<InsertLake_FishKoi_Input> items { get; set; }
    }
}