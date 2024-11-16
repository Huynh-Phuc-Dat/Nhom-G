using System.Collections.Generic;

namespace System_FishKoi.Domain.BO.Lake.Outputs
{
    public class GetDetailLake_Output
    {
        public int LakeID { get; set; }
        public string LakeName { get; set; }
        public float? Depth { get; set; }
        public int? Volume { get; set; }
        public int? QuantityDrain { get; set; }
        public int? PumpOutput { get; set; }
        public float? Size { get; set; }
        public string LakeImage { get; set; }
        public string LakeLink { get; set; }
        public string CreatedUser { get; set; }
        public List<GetListLake_FishKoi_Output> items { get; set; }
    }
}