using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOS.Common.DTO.Request
{
    public class ProductStockRequest
    {
        public int Quantity { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Product")]
        public int ProductId { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
    }
}
