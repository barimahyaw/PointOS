using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;
using PointOS.Common.Enums;
using PointOS.Services;
using System.Threading.Tasks;

namespace PointOS.Pages.Authentication
{
    public partial class ConfirmAccount
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            QueryHelpers.ParseQuery(uri.Query).TryGetValue("token", out var token);
            QueryHelpers.ParseQuery(uri.Query).TryGetValue("userId", out var userId);

            var param = $"?userId={userId}&token={token}";
            var response = await ApiEndpointCallService.CallApiService("Account/ConfirmAccount", null, param, Verb.Post);

            Snackbar.Add(response.Message, response.Success ? Severity.Success : Severity.Error, config => config.ShowCloseIcon = true);

            await Task.Delay(3000);
            NavigationManager.NavigateTo("/");
        }
    }
}
