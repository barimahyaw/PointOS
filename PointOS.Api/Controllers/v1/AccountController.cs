using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Helpers.IHelpers;
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
    public class AccountController : ControllerBase
    {
        private readonly IUserAccountBusiness _userAccountBusiness;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUtils _utils;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccountBusiness"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="utils"></param>
        public AccountController(IUserAccountBusiness userAccountBusiness, IHttpContextAccessor httpContextAccessor, IUtils utils)
        {
            _userAccountBusiness = userAccountBusiness;
            _httpContextAccessor = httpContextAccessor;
            _utils = utils;
        }

        /// <summary>
        /// User Account Authentication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ResponseHeader> Authenticate(AuthenticationRequest request)
            => await _userAccountBusiness.AuthenticationAsync(request, true);

        /// <summary>
        /// User Account Sign Out
        /// </summary>
        [HttpPost("signOut")]
        [AllowAnonymous]
        public new async Task SignOut() => await _userAccountBusiness.SignOutAsync();

        /// <summary>
        /// User Account Authentication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        //[ValidateAntiForgeryToken]
        public async Task<ResponseHeader> Register(UserRegistrationRequest request)
        {
            var response = await _userAccountBusiness.AddUser(request);

            //var confirmLink = Url.Action("ConfirmAccount", "Account",
            //    new { userId = response.ReferenceNumber, token = response.Message }, Request.Scheme);

            //confirmLink = $"Kindly click on the link below to activate your account.</br> {confirmLink}";

            //_utils.EmailSender(request.EmailAddress, "Account Email Confirmation", confirmLink);

            return response;
        }

        /// <summary>
        /// Confirm User Account Email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("confirmEmail")]
        public async Task<ResponseHeader> ConfirmAccount(string userId, string token)
            => await _userAccountBusiness.ConfirmEmail(userId, token);
    }
}
