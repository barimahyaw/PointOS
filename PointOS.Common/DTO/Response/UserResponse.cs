using PointOS.Common.Enums;

namespace PointOS.Common.DTO.Response
{
    public class UserResponse : ResponseBody
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public string RoleName { get; set; }
        public string Department { get; set; }
        public string UserName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {Surname}";
    }
}
