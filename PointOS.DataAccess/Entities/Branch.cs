using System;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        [Required]
        public Guid GuidId { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
    }
}
