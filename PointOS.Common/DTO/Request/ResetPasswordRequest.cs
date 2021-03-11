using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Work email")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
