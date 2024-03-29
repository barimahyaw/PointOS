﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public Guid GuidId { get; set; }
        [Required]
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

        public ICollection<Product> Products { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
