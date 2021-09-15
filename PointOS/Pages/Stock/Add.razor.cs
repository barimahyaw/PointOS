using System.Security.Claims;
using System.Threading.Tasks;
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

namespace PointOS.Pages.Stock
{
    public partial class Add
    {
        protected ProductStockRequest ProductStockRequest { get; set; } = new ProductStockRequest();
        [Inject]
        protected ISessionStorageService SessionStorageService { get; set; }

        //public bool FriendSwitch { get; set; } = true;

        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected async Task SaveChanges()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            ProductStockRequest.CreatedBy = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //ProductStockRequest.Status = FriendSwitch;

            var response = await ApiEndpointCallService.CallApiService("ProductStock", ProductStockRequest, null, Verb.Post);

            Snackbar.Add(response.Message, Severity.Success, config =>
            {
                config.ShowCloseIcon = false;
            });
        }

        protected override async Task OnInitializedAsync()
        {
            var session = await SessionStorageService.GetItemAsync<UserSession>("UserSession");
            var param = $"?companyId={session.CompanyId}";

            var productResponse = await ApiEndpointCallService.CallApiGetService("Product/getAll", null, param);

            var productResult = JsonConvert.DeserializeObject<ListResponse<ProductResponse>>(productResponse.ToString());

            ProductStockRequest.Products = productResult.ResponseBodyList.GetProduct(0);
        }
    }
}
