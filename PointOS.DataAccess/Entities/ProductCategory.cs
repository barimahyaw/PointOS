using System;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public Guid GuidId { get; set; }
    }
}
