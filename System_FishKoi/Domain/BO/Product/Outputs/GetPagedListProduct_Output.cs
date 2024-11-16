﻿namespace System_FishKoi.Domain.BO.Product.Outputs
{
    public class GetPagedListProduct_Output
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public float? Price { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public string ProductLink { get; set; }
        public int TotalRow { get; set; }
    }
}