using System;
using System.Collections.Generic;
using System.Linq;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Newtonsoft.Json;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.DTO.Sessions;
using PointOS.Common.Enums;
using PointOS.Common.Extensions;
using PointOS.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PointOS.Pages.Pricing
{
    public partial class Add
    {
        [Inject]
        protected ISessionStorageService SessionStorageService { get; set; }

        public bool FriendSwitch { get; set; } = true;


        [Inject]
        private ISnackbar Snackbar { get; set; }

        private ProductPricingRequest ProductPricingRequest { get; set; } = new ProductPricingRequest();

        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected async Task SaveChanges()
         {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            ProductPricingRequest.CreatedBy = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ProductPricingRequest.Status = FriendSwitch;

            var response = await ApiEndpointCallService.CallApiService("Pricing", ProductPricingRequest, null, Verb.Post);

            Snackbar.Add(response.Message, Severity.Success, config =>
            {
                config.ShowCloseIcon = false;
            });
        }

        public bool IsOverlayVisible { get; set; }

        //protected void Cancel() => ProductPricingRequest = new ProductPricingRequest();

        protected override async Task OnInitializedAsync()
        {
            var session = await SessionStorageService.GetItemAsync<UserSession>("UserSession");
            var param = $"?companyId={session.CompanyId}";

            var productResponse = await ApiEndpointCallService.CallApiGetService("Product/getAll", null, param);

            var productResult = JsonConvert.DeserializeObject<ListResponse<ProductResponse>>(productResponse.ToString());

            ProductPricingRequest.Products = productResult.ResponseBodyList.GetProduct(0);

            var currencyResponse = await ApiEndpointCallService.CallApiGetService("Currency", null, null);

            var currencyResult = JsonConvert.DeserializeObject<ListResponse<CurrencyResponse>>(currencyResponse.ToString());

            ProductPricingRequest.Currencies = currencyResult.ResponseBodyList.GetCurrencies(0);
        }

        protected async Task<IEnumerable<SelectListItem>> CurrenciesSearch(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, don't return values (drop-down will not open)
            return string.IsNullOrEmpty(value) ? ProductPricingRequest.Currencies 
                : ProductPricingRequest.Currencies.Where(x => x.Text.Contains(value, 
                    StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
