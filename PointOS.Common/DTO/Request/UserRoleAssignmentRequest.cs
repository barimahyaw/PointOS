namespace eViSeM.Common.DTO.Request
{
    public class UserRoleAssignmentRequest
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
