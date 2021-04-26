using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class UserRoleRequest
    {
        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; }
    }
}
