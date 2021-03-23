using System;

namespace PointOS.Common.DTO.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public Guid GuidValue { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int ProductCategoryId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedUserId { get; set; }
    }
}