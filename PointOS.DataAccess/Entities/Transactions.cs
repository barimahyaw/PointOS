using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class Transactions
    {
        //public int Id { get; set; }
        //public Guid GuidId { get; set; }
        [Required]
        [MaxLength(12)]
        [Key]
        public string TransactionId { get; set; }
        [Required]
        [MaxLength(30)]
        public string TransactionType { get; set; }
        [Required]
        [MaxLength(30)]
        public string PaymentType { get; set; }
        public double Amount { get; set; }
        [Required]
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        public DateTime CreatedOn { get; set; }

        public ICollection<Sales> Sales { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
