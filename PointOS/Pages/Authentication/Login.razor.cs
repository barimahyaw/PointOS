using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;
using PointOS.Common.DTO.Request;
using PointOS.Common.Enums;
using PointOS.Services;
using PointOS.Services.Authentication;
using System.Threading.Tasks;

namespace PointOS.Pages.Authentication
{
    public partial class Login
    {
        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISessionStorageService SessionStorageService { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public AuthenticationRequest AuthenticationRequest { get; set; } = new AuthenticationRequest
        {
            UserName = "nanabarimah22@gmail.com",
            Password = "P@$$w0rd@1234"
        };

        protected string ErrorMessage { get; set; }
        protected string ButtonSubmitText { get; set; }
        public bool IsOverlayVisible { get; set; }

        protected async Task Authenticate()
        {
            IsOverlayVisible = true;
            ButtonSubmitText = "Loading...";

            var result = await ApiEndpointCallService.CallApiService("Account/Authenticate", AuthenticationRequest, null, Verb.Post);

            if (result.Success)
            {
                await LocalStorageService.SetItemAsync("authToken", result.Data.Token);
                ((AuthStateProvider)AuthenticationStateProvider).NotifyUserAuthentication(result.Data.Token);

                await SessionStorageService.SetItemAsync("UserSession", result.Data);

                var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("ReturnUrl", out var returnUrl))
                {
                    var url = NavigationManager.ToBaseRelativePath(returnUrl);
                    NavigationManager.NavigateTo($"/{url}");
                }

                if (string.IsNullOrWhiteSpace(returnUrl) && result.Data.CompanyId != 0)
                    NavigationManager.NavigateTo("/Personal/Dashboard");
                else if(!string.IsNullOrWhiteSpace(returnUrl) && result.Data.CompanyId == 0)
                    NavigationManager.NavigateTo("/CompanyBranches");

                ButtonSubmitText = "Sign In";
                Snackbar.Add(result.Message, Severity.Success, config => config.ShowCloseIcon = true);
            }
            else
            {
                ButtonSubmitText = "Sign In";
                IsOverlayVisible = false;
                Snackbar.Add(result.Message, Severity.Error, config => config.ShowCloseIcon = true);
            }
        }

    }
}
