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
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Product")]
        public int ProductId { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
        [Required]
        [Display(Name = "Currency")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Currency")]
        public int CurrencyId { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }
        [Required]
        [Display(Name = "Cost Price")]
        [Range(1, double.MaxValue, ErrorMessage = "Provide a Valid Cost Price")]
        public double CostPrice { get; set; }
        [Required]
        [Display(Name = "Whole Sale Price")]
        [Range(1, double.MaxValue, ErrorMessage = "Provide a Valid Whole Sale Price")]
        public double WholeSalePrice { get; set; }
        [Required]
        [Display(Name = "Retail Price")]
        [Range(1, double.MaxValue, ErrorMessage = "Provide a Valid Retail Price")]
        public double RetailPrice { get; set; }
        public bool Status { get; set; }

        public CrudOperation Operation { get; set; }
    }
}
