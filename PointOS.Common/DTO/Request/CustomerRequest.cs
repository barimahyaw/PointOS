using PointOS.Common.Enums;

namespace PointOS.Common.DTO.Request
{
    public class CustomerRequest
    {
        public string NationalIdCardNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }

        public CrudOperation CrudOperation { get; set; }
    }
}