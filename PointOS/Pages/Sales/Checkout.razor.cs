using Microsoft.AspNetCore.Components;
using MudBlazor;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.Pages.Sales
{
    public partial class Checkout
    {
        public bool IsTranCompleted { get; set; }
        public double SubTotal { get; set; }
        public double Tax { get; set; } = 0.0;
        public double Change { get; set; } = 0.0;

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        [Inject]
        private ISnackbar SnackBar { get; set; }

        public async Task SaveChanges()
        {
            IsTranCompleted = true;
            var salesTran = Products.Select(productResponse =>
                new TransactionRequest
                {
                    Amount = productResponse.ProductPrice,
                    ProductPricingId = productResponse.ProductPricingId,
                    Quantity = productResponse.Quantity
                }).ToList();

            var result = await ApiEndpointCallService.CallApiService("Sales", salesTran, null, Verb.Post);

            SnackBar.Add(result.Message, result.Success ? Severity.Success : Severity.Error, config => config.ShowCloseIcon = false);

            IsTranCompleted = false;

            if (result.Success) MudDialog.Close(DialogResult.Ok(true));
        }
        private void Cancel() => MudDialog.Cancel();

        [Parameter]
        public IList<ProductResponse> Products { get; set; } = new List<ProductResponse>();

        protected ProductRequest ProductRequest { get; set; } = new ProductRequest();

    }
}
