using System;

namespace PointOS.Common.DTO.Response
{
    public class ProductPricingResponse : ResponseBody
    {
        public int Id { get; set; }
        public Guid GuidValue { get; set; }
        public string Product { get; set; }
        public double CostPrice { get; set; }
        public double WholeSalePrice { get; set; }
        public double RetailPrice { get; set; }
        public string Status { get; set; }
    }
}
