using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Models;
using System.Threading.Tasks;

namespace PointOS.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationUserModel> LoginV2(AuthenticationRequest userForAuthentication);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseHeader> Login(AuthenticationRequest request);

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        Task Logout();

        /// <summary>
        /// User Registration Button Submit Handler
        /// </summary>
        /// <returns></returns>
        Task<ResponseHeader> Register(UserRegistrationRequest request);

        /// <summary>
        /// Confirm User Account work email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ResponseHeader> ConfirmAccount(string userId, string token);

        /// <summary>
        /// Request forgot password 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseHeader> ForgotPassword(ForgotPasswordRequest request);

        /// <summary>
        /// Reset Account password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseHeader> ResetPassword(ResetPasswordRequest request);
    }
}