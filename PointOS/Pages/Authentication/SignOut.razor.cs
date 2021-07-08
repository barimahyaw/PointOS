using Microsoft.AspNetCore.Components;
using MudBlazor;
using PointOS.Services.Authentication;
using System.Threading.Tasks;

namespace PointOS.Pages.Authentication
{
    public partial class SignOut
    {
        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            Snackbar.Add("Successfully Signed Out", Severity.Success, config => config.ShowCloseIcon = true);
            NavigationManager.NavigateTo("/");
        }
    }
}
