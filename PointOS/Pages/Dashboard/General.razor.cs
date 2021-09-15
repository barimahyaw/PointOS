using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using PointOS.Common.DTO.Response;
using PointOS.Services;
using System.Threading.Tasks;

namespace PointOS.Pages.Dashboard
{
    public partial class General
    {
        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        protected GeneralDashboardResponse GeneralDashboardResponse { get; set; } = new GeneralDashboardResponse();

        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthState;
            if (!authState.User.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/");
            }

            var param = $"?companyId={1002}";

            var response = await ApiEndpointCallService.CallApiGetService("Dashboard/General", null, param);

            var result = JsonConvert.DeserializeObject<SingleResponse<GeneralDashboardResponse>>(response.ToString());

            GeneralDashboardResponse = result.ResponseBody;
        }
    }
}
