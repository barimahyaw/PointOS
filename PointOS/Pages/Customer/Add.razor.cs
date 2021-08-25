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

namespace PointOS.Pages.Customer
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

        private CustomerRequest CustomerRequest { get; set; } = new CustomerRequest();

        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected async Task SaveChanges()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            CustomerRequest.CreatedBy = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            CustomerRequest.Status = FriendSwitch;

            var response = await ApiEndpointCallService.CallApiService("Customer", CustomerRequest, null, Verb.Post);

            Snackbar.Add(response.Message, response.Success ? Severity.Success : Severity.Error, config =>
            {
                config.ShowCloseIcon = false;
            });

            if (response.Success) MudDialog.Close(DialogResult.Ok(true));
        }
    }
}

