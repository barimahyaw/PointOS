using System;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class ProductStock
    {
        public int Id { get; set; }
        [Required]
        public Guid GuidId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        [Required]
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        public string ModifiedUserId { get; set; }
        public ApplicationUser ModifiedUser { get; set; }
    }
}
