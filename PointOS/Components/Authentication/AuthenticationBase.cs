using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using System.Threading.Tasks;

namespace PointOS.Components.Authentication
{
    public class AuthenticationBase : PasswordVisibilityBase
    {
        [Inject]
        private IRestUtility RestUtility { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public AuthenticationRequest AuthenticationRequest { get; set; } = new AuthenticationRequest();
        protected UserRegistrationRequest UserRegistrationRequest { get; set; } = new UserRegistrationRequest();
        protected string ErrorMessage { get; set; }
        protected string ButtonSubmitText { get; set; } 

        /// <summary>
        /// Authentication/Login Button Submit Handler
        /// </summary>
        /// <returns></returns>
        protected async Task Authenticate()
        {
            ButtonSubmitText = "Loading...";

            var response = await RestUtility.ApiServiceAsync(BaseUrl.PointOs, "Account/Authenticate", null, AuthenticationRequest,
                null, Verb.Post);

            var result = JsonConvert.DeserializeObject<ResponseHeader>(response.ToString());

            ErrorMessage = result.Message;

            if (result.Success)
            {
                ButtonSubmitText = "Sign In";
                Snackbar.Add(result.Message, Severity.Success, config => config.ShowCloseIcon = true);
                NavigationManager.NavigateTo("/Personal/Dashboard");
            }
            else
            {
                ButtonSubmitText = "Sign In";
                Snackbar.Add(result.Message, Severity.Error, config => config.ShowCloseIcon = true);
            }
        }

        /// <summary>
        /// User Registration Button Submit Handler
        /// </summary>
        /// <returns></returns>
        protected async Task Register()
        {
            ButtonSubmitText = "Submitting...";

            var response = await RestUtility.ApiServiceAsync(BaseUrl.PointOs, "Account/Register", null, UserRegistrationRequest,
                null, Verb.Post);

            var result = JsonConvert.DeserializeObject<ResponseHeader>(response.ToString());

            ErrorMessage = result.Message;

            if (result.Success)
                NavigationManager.NavigateTo("/");
            else
            {
                ButtonSubmitText = "Register";
                Snackbar.Add(result.Message, Severity.Error, config => config.ShowCloseIcon = false);
            }
        }
    }
}
