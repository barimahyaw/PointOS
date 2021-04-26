using Microsoft.AspNetCore.Mvc.Rendering;
using PointOS.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class BranchRequest : RequestBody
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }

        public CrudOperation Operation { get; set; }
    }
}
