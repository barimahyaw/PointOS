using PointOS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class CustomerRequest : RequestBody
    {
        [Required]
        public string NationalIdCardNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string Address { get; set; }

        public CrudOperation CrudOperation { get; set; }
        public bool Status { get; set; }
    }
}