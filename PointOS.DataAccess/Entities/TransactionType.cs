using System;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class TransactionType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        [Required]
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedUserId { get; set; }
        public ApplicationUser ModifiedUser { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
