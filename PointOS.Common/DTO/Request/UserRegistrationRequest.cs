using PointOS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class UserRegistrationRequest
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Required]
        public string Surname { get; set; }

        //[Required]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress]
        [ConcurrencyCheck]
        [Display(Name = "Work email")]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public Gender Gender { get; set; }

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Phone No. is Required")]
        [Display(Name = "Phone No.")]
        public string PhoneNumber { get; set; }

        //public int DepartmentId { get; set; }
    }
}
