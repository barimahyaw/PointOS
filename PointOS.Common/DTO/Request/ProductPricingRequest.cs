using Microsoft.AspNetCore.Mvc.Rendering;
using PointOS.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class ProductPricingRequest : RequestBody
    {
        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
        [Required]
        [Display(Name = "Cost Price")]
        public double CostPrice { get; set; }
        [Required]
        [Display(Name = "Whole Sale Price")]
        public double WholeSalePrice { get; set; }
        [Required]
        [Display(Name = "Retail Price")]
        public double RetailPrice { get; set; }
        public bool Status { get; set; }

        public CrudOperation Operation { get; set; }
    }
}
