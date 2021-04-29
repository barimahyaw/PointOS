using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class TransactionRequest
    {
        public int Quantity { get; set; }
        public double Amount { get; set; }
        [Required]
        [Display(Name = "Product")]
        public int ProductPricingId { get; set; }
        public IEnumerable<SelectListItem> PProductPricing { get; set; }
    }
}
