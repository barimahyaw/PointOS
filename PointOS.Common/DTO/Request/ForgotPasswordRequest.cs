using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Work email")]
        public string EmailAddress { get; set; }
    }
}
