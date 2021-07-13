using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;
using PointOS.Common.DTO.Request;
using PointOS.Services.Authentication;
using System.Threading.Tasks;

namespace PointOS.Pages.Authentication
{
    public partial class Login
    {
        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public AuthenticationRequest AuthenticationRequest { get; set; } = new AuthenticationRequest();

        protected string ErrorMessage { get; set; }
        protected string ButtonSubmitText { get; set; }
        public bool IsOverlayVisible { get; set; }

        protected async Task Authenticate()
        {
            IsOverlayVisible = true;
            ButtonSubmitText = "Loading...";
            var response = await AuthenticationService.Login(AuthenticationRequest);

            if (response.Success)
            {
                var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("ReturnUrl", out var returnUrl))
                {
                    var url = NavigationManager.ToBaseRelativePath(returnUrl);
                    NavigationManager.NavigateTo($"/{url}");
                }

                if (string.IsNullOrWhiteSpace(returnUrl))
                    NavigationManager.NavigateTo("/Personal/Dashboard");

                ButtonSubmitText = "Sign In";
                Snackbar.Add(response.Message, Severity.Success, config => config.ShowCloseIcon = true);
            }
            else
            {
                ButtonSubmitText = "Sign In";
                IsOverlayVisible = false;
                Snackbar.Add(response.Message, Severity.Error, config => config.ShowCloseIcon = true);
            }
        }

    }
}
