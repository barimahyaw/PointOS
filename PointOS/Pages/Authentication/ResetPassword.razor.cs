using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;
using PointOS.Common.DTO.Request;
using PointOS.Common.Enums;
using PointOS.Services;
using System.Threading.Tasks;

namespace PointOS.Pages.Authentication
{
    public partial class ResetPassword
    {
        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public ResetPasswordRequest ResetPasswordRequest { get; set; } = new ResetPasswordRequest();

        protected string ErrorMessage { get; set; }

        protected string ButtonSubmitText { get; set; } = "Change Password";
        public bool IsOverlayVisible { get; set; }

        protected async Task SubmitRequest()
        {
            IsOverlayVisible = true;
            ButtonSubmitText = "Submitting...";

            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            QueryHelpers.ParseQuery(uri.Query).TryGetValue("EmailAddress", out var emailAddress);
            QueryHelpers.ParseQuery(uri.Query).TryGetValue("token", out var token);

            var request = new ResetPasswordRequest
            {
                EmailAddress = emailAddress,
                ConfirmPassword = ResetPasswordRequest.ConfirmPassword,
                Password = ResetPasswordRequest.Password,
                Token = token
            };

            var result = await ApiEndpointCallService.CallApiService("Account/ResetPassword", ResetPasswordRequest, null, Verb.Post);

            ErrorMessage = result.Message;

            Snackbar.Add(result.Message, result.Success ? Severity.Success : Severity.Error, config => config.ShowCloseIcon = false);

            if (result.Success)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                IsOverlayVisible = false;
                ButtonSubmitText = "Change Password";
            }

        }
    }
}
