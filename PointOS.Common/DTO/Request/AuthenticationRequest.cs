using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class AuthenticationRequest
    {
        [Required]
        [Display(Name = "Work email")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string Id { get; set; }
    }
}
