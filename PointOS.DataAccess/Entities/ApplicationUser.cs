using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PointOS.DataAccess.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int Gender { get; set; }
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        public bool IsActive { get; set; }
    }
}
