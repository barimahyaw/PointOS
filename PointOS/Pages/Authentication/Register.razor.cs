﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using PointOS.Common.DTO.Request;
using PointOS.Common.Enums;
using PointOS.Services;
using PointOS.Services.Notifications;
using System.Threading.Tasks;

namespace PointOS.Pages.Authentication
{
    public partial class Register
    {
        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }


        [Inject]
        private ISnackbar Snackbar { get; set; }

        public UserRegistrationRequest UserRegistrationRequest { get; set; } = new UserRegistrationRequest();

        protected string ErrorMessage { get; set; }
        protected string ButtonSubmitText { get; set; }
        public bool IsOverlayVisible { get; set; }

        [Inject]
        private IEmailService EmailService { get; set; }

        protected async Task SubmitRegistration()
        {
            IsOverlayVisible = true;
            ButtonSubmitText = "Submitting...";

            var result = await ApiEndpointCallService.CallApiService("Account/Register", UserRegistrationRequest, null, Verb.Post);

            ErrorMessage = result.Message;

            if (result.Success)
            {
                var confirmLink = NavigationManager.ToAbsoluteUri($"Account/ConfirmAccount?userId={result.ReferenceNumber}&token={result.Data.Token}").ToString();

                confirmLink = $"Kindly click on the link below to activate your account.</br>{confirmLink}";

                var emailRequest = new EmailRequest
                {
                    EmailAddress = UserRegistrationRequest.EmailAddress,
                    Subject = "Account Email Confirmation",
                    Body = confirmLink
                };

                await EmailService.SendEmail(emailRequest);

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
