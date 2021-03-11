using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class RoleRegistrationRequest
    {
        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; }
    }
}
