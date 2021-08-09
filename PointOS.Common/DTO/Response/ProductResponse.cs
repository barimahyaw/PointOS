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
    }
}