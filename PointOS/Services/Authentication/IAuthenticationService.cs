using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Models;
using System.Threading.Tasks;

namespace PointOS.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationUserModel> LoginV2(AuthenticationRequest userForAuthentication);
        Task<ResponseHeader> Login(AuthenticationRequest request);
        Task Logout();

        /// <summary>
        /// User Registration Button Submit Handler
        /// </summary>
        /// <returns></returns>
        Task<ResponseHeader> Register(UserRegistrationRequest request);
    }
}