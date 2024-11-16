namespace System_FishKoi.Domain.BO.Product.Inputs
{
    public class UpdateProduct_Input
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public float? Price { get; set; }
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
    }
}