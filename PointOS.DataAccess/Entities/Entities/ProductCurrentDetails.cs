using System;

namespace PointOS.DataAccess.Entities.Entities
{
    public class ProductCurrentDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedUserId { get; set; }
        public string CreatedUser { get; set; }
        public string ProductCategory { get; set; }
    }
}
