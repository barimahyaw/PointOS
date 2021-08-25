namespace PointOS.Common.DTO.Response
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

    }
}
