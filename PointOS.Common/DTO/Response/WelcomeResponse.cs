namespace PointOS.Common.DTO.Response
{
    public class WelcomeResponse
    {
        public string UserId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string CompanyAbbrev { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LogoPath { get; set; }
        public int EmpCount { get; set; }
        public string PhotoPath { get; set; }
        public int EmployeeId { get; set; }
    }
}