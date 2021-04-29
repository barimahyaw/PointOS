﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class Sales
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string TransactionId { get; set; }
        [Required]
        [MaxLength(30)]
        public string PaymentType { get; set; }
        [Required]
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}