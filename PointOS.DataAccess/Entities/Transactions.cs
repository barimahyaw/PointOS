using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class Transactions
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string TransactionType { get; set; }
        [Required]
        [MaxLength(12)]
        public string TransactionId { get; set; }
        public int Quantity { get; set; }
        public int? ProductPricingId { get; set; }
        public ProductPricing ProductPricing { get; set; }
        public double Amount { get; set; }
    }
}
