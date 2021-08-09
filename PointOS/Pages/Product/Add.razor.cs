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

namespace PointOS.Pages.Product
{
    public partial class Add
    {
        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Inject]
        protected ISessionStorageService SessionStorageService { get; set; }
        private void Cancel() => MudDialog.Cancel();

        public bool FriendSwitch { get; set; } = true;

        //public string Message { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        private ProductRequest ProductRequest { get; set; } = new ProductRequest();

        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected async Task SaveChanges()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            ProductRequest.CreatedBy = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ProductRequest.Status = FriendSwitch;

            var response = await ApiEndpointCallService.CallApiService("Product", ProductRequest, null, Verb.Post);

            Snackbar.Add(response.Message, Severity.Success, config =>
            {
                config.ShowCloseIcon = false;
            });

            MudDialog.Close(DialogResult.Ok(true));
        }

        protected override async Task OnInitializedAsync()
        {
            var session = await SessionStorageService.GetItemAsync<UserSession>("UserSession");

            var param = $"?companyId={session.CompanyId}";

            var response = await ApiEndpointCallService.CallApiGetService("ProductCategory/find", null, param);

            var result = JsonConvert.DeserializeObject<ListResponse<ProductCategoryResponse>>(response.ToString());

            ProductRequest.ProductCategories = result.ResponseBodyList.GetProductCategories(0);
        }
    }
}
