using System;

namespace PointOS.Common.DTO.Response
{
    public class ProductResponse : ResponseBody
    {
        public int Id { get; set; }
        public Guid GuidValue { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }
        public int Stock { get; set; }
        public double CurrentRetailPrice { get; set; }
        public double PreviousRetailPrice { get; set; }
        public double ProductPrice { get; set; }
        public double Tax { get; set; }
        public int Quantity { get; set; } = 1;
        public string PhotoPath { get; set; }
        public int ProductPricingId { get; set; }
    }
}