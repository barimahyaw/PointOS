using Microsoft.AspNetCore.Components;
using MudBlazor;
using PointOS.Common.DTO.Request;
using PointOS.Common.Enums;
using PointOS.Services;
using PointOS.Services.Notifications;
using System.Threading.Tasks;

namespace PointOS.Pages.Company
{
    public partial class Register
    {
        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        public CompanyRegistrationRequest CompanyRegistrationRequest { get; set; }
            = new CompanyRegistrationRequest { CompanyRequest = new CompanyRequest(), UserRegistrationRequest = new UserRegistrationRequest() };

        protected string ErrorMessage { get; set; }

        protected string ButtonSubmitText { get; set; }

        public bool IsOverlayVisible { get; set; }

        public bool IsCompanyVisible { get; set; } = true;

        [Inject]
        private NavigationManager NavigationManager { get; set; }


        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Inject]
        private IEmailService EmailService { get; set; }

        protected void NextPage() => IsCompanyVisible = false;

        protected async Task SubmitRegistration()
        {
            IsOverlayVisible = true;
            ButtonSubmitText = "Submitting...";

            var result = await ApiEndpointCallService.CallApiService("Company", CompanyRegistrationRequest, null, Verb.Post);

            ErrorMessage = result.Message;

            if (result.Success)
            {
                var confirmLink = NavigationManager.ToAbsoluteUri($"Account/ConfirmAccount?userId={result.ReferenceNumber}&token={result.Data.Token}").ToString();

                confirmLink = $"Kindly click on the link below to activate your account.<br/>{confirmLink}";

                var userEmailRequest = new EmailRequest
                {
                    EmailAddress = CompanyRegistrationRequest.UserRegistrationRequest.EmailAddress,
                    Subject = "Account Email Confirmation",
                    Body = confirmLink
                };
                await EmailService.SendEmail(userEmailRequest);

                var companyEmailRequest = new EmailRequest
                {
                    EmailAddress = CompanyRegistrationRequest.CompanyRequest.EmailAddress,
                    Subject = "Request for Demo",
                    Body = $"Hi {CompanyRegistrationRequest.CompanyRequest.Name}, <br/> Thanks for submitting a request to receive a demo of PointOS through our website."
                };
                await EmailService.SendEmail(companyEmailRequest);

                Snackbar.Add("Account confirmation Link sent to your work email successfully.", Severity.Success, config => config.ShowCloseIcon = false);

                NavigationManager.NavigateTo("/");
            }
            else
            {
                IsOverlayVisible = false;
                ButtonSubmitText = "Register";
                Snackbar.Add(result.Message, Severity.Error, config => config.ShowCloseIcon = false);
            }
        }
    }
}
