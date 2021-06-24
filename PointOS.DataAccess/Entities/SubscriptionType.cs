using System;
using System.ComponentModel.DataAnnotations;

namespace PointOS.DataAccess.Entities
{
    public class SubscriptionType
    {
        public int Id { get; set; }
        //[Required]
        //public Guid GuidId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
