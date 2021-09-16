using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public Guid GuidId { get; set; }
        [Required]
        [MaxLength(50)]
        public string NationalIdCardNumber { get; set; }
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(50)]
        [Required]
        public string PhoneNumber { get; set; }
        [MaxLength(225)]
        public string EmailAddress { get; set; }
        [MaxLength(225)]
        [Required]
        public string Address { get; set; }
        [Required]
        public string CreatedUerId { get; set; }
        public ApplicationUser CreatedUer { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedUerId { get; set; }
        public ApplicationUser ModifiedUer { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public ICollection<Transactions> Sales { get; set; }
    }
}
