using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;
using PointOS.Services.Authentication;
using System.Threading.Tasks;

namespace PointOS.Pages.Authentication
{
    public partial class ConfirmAccount
    {
        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            QueryHelpers.ParseQuery(uri.Query).TryGetValue("token", out var token);
            QueryHelpers.ParseQuery(uri.Query).TryGetValue("userId", out var userId);

            var response = await AuthenticationService.ConfirmAccount(userId, token);
            Snackbar.Add(response.Message, response.Success ? Severity.Success : Severity.Error, config => config.ShowCloseIcon = true);

            await Task.Delay(3000);
            NavigationManager.NavigateTo("/");
        }
    }
}
