using System;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool Status { get; set; }
        [Required]
        public Guid GuidId { get; set; }
        [Required]
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
