namespace PointOS.Common.DTO.Sessions
{
    public class UserSession
    {
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int BranchId { get; set; }
        public string Branch { get; set; }
        public string FullName { get; set; }
        public string PhotoPath { get; set; }
        public string LogoPath { get; set; }
    }
}
