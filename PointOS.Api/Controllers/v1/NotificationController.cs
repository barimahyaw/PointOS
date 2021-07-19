using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOS.Common.DTO.Request;
using PointOS.Common.Helpers.IHelpers;

namespace PointOS.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "CustomAuthentication")]
    public class NotificationController : ControllerBase
    {
        private readonly IUtils _utils;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="utils"></param>
        public NotificationController(IUtils utils)
        {
            _utils = utils;
        }

        /// <summary>
        /// smtp email sender end point
        /// </summary>
        /// <param name="request"></param>
        [AllowAnonymous]
        [HttpPost("sendEmail")]
        public void SendEmail(EmailRequest request) => _utils.EmailSender(request);
    }
}
