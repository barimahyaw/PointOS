using System;
using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class ProductCategoryRequest : RequestBody
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public bool Status { get; set; }
        public Guid GuidId { get; set; }
    }
}
