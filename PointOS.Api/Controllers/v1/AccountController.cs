using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
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
    public class AccountController : ControllerBase
    {
        private readonly IUserAccountBusiness _userAccountBusiness;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccountBusiness"></param>
        public AccountController(IUserAccountBusiness userAccountBusiness)
            => _userAccountBusiness = userAccountBusiness;

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
        [HttpPost("confirmAccount")]
        public async Task<ResponseHeader> ConfirmAccount(string userId, string token)
            => await _userAccountBusiness.ConfirmEmail(userId, token);

        /// <summary>
        /// Request forgot password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        public async Task<ResponseHeader> ForgotPassword(ForgotPasswordRequest request)
            => await _userAccountBusiness.ForgotPasswordAsync(request);

        /// <summary>
        /// Reset Account password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("resetPassword")]
        public async Task<ResponseHeader> ResetPassword(ResetPasswordRequest request)
            => await _userAccountBusiness.ResetPasswordAsync(request);
    }
}
