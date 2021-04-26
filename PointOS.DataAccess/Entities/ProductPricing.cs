using System;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class ProductPricing
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public double CostPrice { get; set; }
        public double WholeSalePrice { get; set; }
        public double RetailPrice { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public Guid GuidId { get; set; }
        [Required]
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

    }
}
