using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class ProductRequest
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public bool Status { get; set; }
        public int ProductCategoryId { get; set; }
        public string CreatedUserId { get; set; }
    }
}