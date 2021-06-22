using Microsoft.AspNetCore.Components;
using MudBlazor;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using System.Threading.Tasks;

namespace PointOS.Components.Authentication
{
    public class LogoutBase : ComponentBase
    {
        [Inject]
        private IRestUtility RestUtility { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await RestUtility.ApiServiceAsync(BaseUrl.PointOs, "Account/SignOut", null, null, null, Verb.Post);

            Snackbar.Add("Successfully Signed Out", Severity.Success, config => config.ShowCloseIcon = true);
            NavigationManager.NavigateTo("/");
        }
    }
}
