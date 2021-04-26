using PointOS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class CompanyRequest : RequestBody
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(20)]
        public string AltPhoneNumber { get; set; }
        public string CreatedUserId { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        [Display(Name = "Work email")]
        public string EmailAddress { get; set; }

        public CrudOperation Operation { get; set; }
    }
}
