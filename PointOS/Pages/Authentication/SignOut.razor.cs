//using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PointOS.Services.Authentication;
using System.Threading.Tasks;

namespace PointOS.Pages.Authentication
{
    public partial class SignOut
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }


        [Inject]
        private ISessionStorageService SessionStorageService { get; set; }

        //[Inject]
        //public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await SessionStorageService.RemoveItemAsync("UserSession");
            //await LocalStorageService.RemoveItemAsync("authToken");
            ((AuthStateProvider)AuthenticationStateProvider).NotifyUserLogout();

            Snackbar.Add("Successfully Signed Out", Severity.Success, config => config.ShowCloseIcon = true);
            NavigationManager.NavigateTo("/");
        }
    }
}
