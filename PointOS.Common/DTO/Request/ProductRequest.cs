using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Product Category")]
        public int ProductCategoryId { get; set; }
        public IEnumerable<SelectListItem> ProductCategories { get; set; }

        public int Quantity { get; set; }


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
        [Required]
        [Display(Name = "Currency")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Currency")]
        public int CurrencyId { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }
    }
}