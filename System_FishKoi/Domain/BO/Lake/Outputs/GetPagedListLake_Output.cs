namespace System_FishKoi.Domain.BO.Lake.Outputs
{
    public class GetPagedListLake_Output
    {
        public int LakeID { get; set; }
        public string LakeName { get; set; }
        public float? Depth { get; set; }
        public int? Volume { get; set; }
        public int? QuantityDrain { get; set; }
        public int? PumpOutput { get; set; }
        public int TotalRow { get; set; }
        public bool IsDeleted { get; set; }
        public int? Size { get; set; }
        public string CreatedUser { get; set; }
    }
}