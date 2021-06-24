using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

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

        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }

        public AuthenticationRequest AuthenticationRequest { get; set; } = new AuthenticationRequest();
        protected UserRegistrationRequest UserRegistrationRequest { get; set; } = new UserRegistrationRequest();
        protected string ErrorMessage { get; set; }
        protected string ButtonSubmitText { get; set; }
        public bool IsOverlayVisible { get; set; }

        //[Inject]
        //private IHttpContextAccessor HttpContextAccessor { get; set; } = new HttpContextAccessor();
        /// <summary>
        /// Authentication/Login Button Submit Handler
        /// </summary>
        /// <returns></returns>
        protected async Task Authenticate()
        {
            IsOverlayVisible = true;
            ButtonSubmitText = "Loading...";

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type","password"),
                new KeyValuePair<string, string>("username",AuthenticationRequest.UserName),
                new KeyValuePair<string, string>("grant_type",AuthenticationRequest.Password)
            });

            var response = await RestUtility.ApiServiceAsync(BaseUrl.PointOs, "Account/Authenticate", null, AuthenticationRequest,
                null, Verb.Post);

            var result = JsonConvert.DeserializeObject<ResponseHeader>(response.ToString());

            ErrorMessage = result.Message;

            if (result.Success)
            {
                await LocalStorage.SetItemAsync("authToken", $"Bearer {result.Data.Token}");
                ButtonSubmitText = "Sign In";
                Snackbar.Add(result.Message, Severity.Success, config => config.ShowCloseIcon = true);
                NavigationManager.NavigateTo("/Personal/Dashboard");
            }
            else
            {
                //var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                //var user = authState.User;
                ButtonSubmitText = "Sign In";
                IsOverlayVisible = false;
                Snackbar.Add(result.Message, Severity.Error, config => config.ShowCloseIcon = true);
            }
        }

        /// <summary>
        /// User Registration Button Submit Handler
        /// </summary>
        /// <returns></returns>
        protected async Task Register()
        {
            IsOverlayVisible = true;
            ButtonSubmitText = "Submitting...";

            var response = await RestUtility.ApiServiceAsync(BaseUrl.PointOs, "Account/Register", null, UserRegistrationRequest,
                null, Verb.Post);

            var result = JsonConvert.DeserializeObject<ResponseHeader>(response.ToString());

            ErrorMessage = result.Message;

            if (result.Success)
                NavigationManager.NavigateTo("/");
            else
            {
                IsOverlayVisible = false;
                ButtonSubmitText = "Register";
                Snackbar.Add(result.Message, Severity.Error, config => config.ShowCloseIcon = false);
            }
        }
    }
}
