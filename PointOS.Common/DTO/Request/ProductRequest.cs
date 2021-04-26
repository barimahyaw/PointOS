﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class ProductRequest : RequestBody
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public bool Status { get; set; }
        [Required]
        [Display(Name = "Product Category")]
        public int ProductCategoryId { get; set; }
        public string CreatedUserId { get; set; }

        public IEnumerable<SelectListItem> ProductCategories { get; set; }
    }
}