using System;

namespace PointOS.Common.DTO.Response
{
    public class ProductCategoryResponse : ResponseBody
    {
        public int Id { get; set; }
        public Guid GuidValue { get; set; }
        public string ProductName { get; set; }
        public bool Status { get; set; }

    }
}