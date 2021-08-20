using Microsoft.AspNetCore.Components;
using PointOS.Common.DTO.Response;
using System.Collections.Generic;

namespace PointOS.Pages.Sales
{
    public partial class Sales
    {
        [Parameter]
        public IList<ProductResponse> Products { get; set; } = new List<ProductResponse>();

        [Parameter]
        public int ProductId { get; set; }

        public void Refresh() => StateHasChanged();

        public void ClearProducts()
        {
            Products = new List<ProductResponse>();
            StateHasChanged();
        }
    }
}
