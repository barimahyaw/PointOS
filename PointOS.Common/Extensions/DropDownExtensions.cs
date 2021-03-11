using Microsoft.AspNetCore.Mvc.Rendering;
using PointOS.Common.DTO.Response;
using System.Collections.Generic;
using System.Linq;

namespace PointOS.Common.Extensions
{
    public static class DropDownExtensions
    {
        /// <summary>
        /// Dropdown extension method for product category entity
        /// </summary>
        /// <param name="productCategory"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetCompanies(this IEnumerable<ProductCategoryResponse> productCategory,
            int selected)
        {
            var items = productCategory.OrderBy(x => x.Id).Select(c => new SelectListItem
            {
                Selected = c.Id == selected,
                Text = c.ProductName,
                Value = c.Id.ToString()
            }).ToList();

            items.Insert(0, new SelectListItem { Text = "-- Select Product Category --", Value = "", Selected = true });

            return items;
        }
    }
}
