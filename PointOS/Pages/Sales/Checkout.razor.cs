using Microsoft.AspNetCore.Components;
using MudBlazor;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using System.Collections.Generic;

namespace PointOS.Pages.Sales
{
    public partial class Checkout
    {
        public double SubTotal { get; set; }
        public double Tax { get; set; } = 0.0;
        public double Change { get; set; } = 0.0;

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }
        public void SaveChanges() { }
        private void Cancel() => MudDialog.Cancel();

        [Parameter]
        public IList<ProductResponse> Products { get; set; } = new List<ProductResponse>();

        protected ProductRequest ProductRequest { get; set; } = new ProductRequest();
    }
}
