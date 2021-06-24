using System;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class Sales
    {
        public int Id { get; set; }
        [Required]
        public Guid GuidId { get; set; }
        [Required]
        [MaxLength(20)]
        public string TransactionId { get; set; }
        public Transactions Transaction { get; set; }
        public int Quantity { get; set; }
        public int ProductPricingId { get; set; }
        public ProductPricing ProductPricing { get; set; }
    }
}
