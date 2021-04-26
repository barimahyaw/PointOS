using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public Guid GuidId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(20)]
        public string AltPhoneNumber { get; set; }
        [Required]
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<Branch> Branches { get; set; }
    }
}
