using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Helpers.IHelpers;
using PointOS.Services.Authentication;
using System.Threading.Tasks;

namespace PointOS.Pages.Authentication
{
    public partial class Register
    {
        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUtils Utils { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        public UserRegistrationRequest UserRegistrationRequest { get; set; } = new UserRegistrationRequest();

        protected string ErrorMessage { get; set; }
        protected string ButtonSubmitText { get; set; }
        public bool IsOverlayVisible { get; set; }


        protected async Task SubmitRegistration()
        {
            IsOverlayVisible = true;
            ButtonSubmitText = "Submitting...";

            var response = await AuthenticationService.Register(UserRegistrationRequest);

            var result = JsonConvert.DeserializeObject<ResponseHeader>(response.ToString());

            ErrorMessage = result.Message;

            if (result.Success)
            {
                //var confirmLink = Url.Action("ConfirmAccount", "Account",
                //    new { userId = response.ReferenceNumber, token = response.Message }, Request.Scheme);

                var confirmLink = NavigationManager.ToAbsoluteUri($"Account/ConfirmAccount?userId={result.ReferenceNumber}&token={result.Data.Token}").ToString();

                confirmLink = $"Kindly click on the link below to activate your account.</br>{confirmLink}";

                Utils.EmailSender(UserRegistrationRequest.EmailAddress, "Account Email Confirmation", confirmLink);
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
