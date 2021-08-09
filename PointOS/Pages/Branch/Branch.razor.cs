using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PointOS.Common.DTO.Request;
using PointOS.Common.Enums;
using PointOS.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PointOS.Pages.Branch
{
    public partial class Branch
    {
        private BranchRequest BranchRequest { get; set; } = new BranchRequest();

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        protected string ErrorMessage { get; set; }

        protected string ButtonSubmitText { get; set; }

        public bool IsOverlayVisible { get; set; }

        protected async Task SaveChanges()
        {
            IsOverlayVisible = true;
            ButtonSubmitText = "Submitting...";

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            BranchRequest.CreatedBy = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            BranchRequest.CompanyId = 1002;

            var result = await ApiEndpointCallService.CallApiService("Branch", BranchRequest, null, Verb.Post);

            ErrorMessage = result.Message;

            Snackbar.Add(result.Message, result.Success ? Severity.Success : Severity.Error, config => config.ShowCloseIcon = false);

            MudDialog.Close(DialogResult.Ok(true));

        }
    }
}
