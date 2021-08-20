using Microsoft.AspNetCore.Components;
using MudBlazor;
using PointOS.Common.DTO.Request;
using PointOS.Common.Enums;
using PointOS.Services;
using PointOS.Services.Notifications;
using System.Threading.Tasks;

namespace PointOS.Pages.Authentication
{
    public partial class ForgotPassword
    {
        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar SnackBar { get; set; }

        public ForgotPasswordRequest ForgotPasswordRequest { get; set; } = new ForgotPasswordRequest();

        protected string ErrorMessage { get; set; }
        protected string ButtonSubmitText { get; set; } = "Reset Password";
        public bool IsOverlayVisible { get; set; }

        [Inject]
        private IEmailService EmailService { get; set; }

        protected async Task SubmitRequest()
        {
            IsOverlayVisible = true;
            ButtonSubmitText = "Submitting...";

            var result = await ApiEndpointCallService.CallApiService("Account/ForgotPassword", ForgotPasswordRequest, null, Verb.Post);

            //ErrorMessage = result.Message;

            if (result.Success)
            {
                var confirmLink = NavigationManager.ToAbsoluteUri($"Account/ResetPassword?EmailAddress={ForgotPasswordRequest.EmailAddress}&token={result.Data.Token}").ToString();

                confirmLink = $"Kindly click on the link below to Reset your account password.</br>{confirmLink}";

                var emailRequest = new EmailRequest
                {
                    EmailAddress = ForgotPasswordRequest.EmailAddress,
                    Subject = "Password Reset Instruction",
                    Body = confirmLink
                };

                await EmailService.SendEmail(emailRequest);

                SnackBar.Add("Password reset Link sent to your work email successfully.", Severity.Success, config => config.ShowCloseIcon = true);

                NavigationManager.NavigateTo("/");
            }
            else
            {
                IsOverlayVisible = false;
                ButtonSubmitText = "Reset Password";
                SnackBar.Add(result.Message, Severity.Error, config => config.ShowCloseIcon = false);
                ForgotPasswordRequest.EmailAddress = string.Empty;
            }
        }
    }
}
