using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PointOS.DataAccess.Entities
{
    public class Sales
    {
        public int Id { get; set; }
        [Required]
        public Guid GuidId { get; set; }
        [Required]
        [MaxLength(12)]
        [ForeignKey("Transaction")]
        public string TransactionId { get; set; }
        public Transactions Transaction { get; set; }
        public int Quantity { get; set; }
        public int ProductPricingId { get; set; }
        public ProductPricing ProductPricing { get; set; }
    }
}
