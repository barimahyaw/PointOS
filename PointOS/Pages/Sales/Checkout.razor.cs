using Microsoft.AspNetCore.Components;
using MudBlazor;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PointOS.Pages.Sales
{
    public partial class Checkout
    {
        public bool IsTranCompleted { get; set; }
        public double SubTotal { get; set; }
        public double Tax { get; set; } = 0.0;
        public double Change { get; set; } = 0.0;
        protected string CustomerPhoneNumber { get; set; }
        protected string PaymentType { get; set; }

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

            var param = $"?customerPhoneNumber={CustomerPhoneNumber}";

            var result = await ApiEndpointCallService.CallApiService("Sales", salesTran, param, Verb.Post);

            SnackBar.Add(result.Message, result.Success ? Severity.Success : Severity.Error, config => config.ShowCloseIcon = false);

            IsTranCompleted = false;

            if (result.Success) MudDialog.Close(DialogResult.Ok(true));
        }
        private void Cancel() => MudDialog.Cancel();

        [Parameter]
        public IList<ProductResponse> Products { get; set; } = new List<ProductResponse>();

        protected ProductRequest ProductRequest { get; set; } = new ProductRequest();

        protected void SetCustomer(ChangeEventArgs eventArgs) => ProductRequest.CostPrice = Convert.ToDouble(eventArgs.Value);

        private async Task<IEnumerable<string>> Search2(string value)
        {
            await Task.Delay(10);
            var param = $"?phoneNumber={value}";

            var response = await ApiEndpointCallService.CallApiGetService("Customer/GetAllLikePhoneNumber", null, param);

            var result = JsonConvert.DeserializeObject<ListResponse<CustomerResponse>>(response.ToString());

            var phoneNumbers = result.ResponseBodyList.Select(x => x.PhoneNumber);
           
            return phoneNumbers;
        }
    }
}
