using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Response;
using System.Threading.Tasks;

namespace PointOS.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "CustomAuthentication")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardBusiness _dashboardBusiness;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dashboardBusiness"></param>
        public DashboardController(IDashboardBusiness dashboardBusiness) => _dashboardBusiness = dashboardBusiness;

        /// <summary>
        /// Gets data for the general company's dashboard
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet("general")]
        [AllowAnonymous]
        public async Task<SingleResponse<GeneralDashboardResponse>> General(int companyId) =>
            await _dashboardBusiness.General(companyId);
    }
}
