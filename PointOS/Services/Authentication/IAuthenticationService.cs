using System.Threading.Tasks;
using PointOS.Common.DTO.Request;
using PointOS.Models;

namespace PointOS.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationUserModel> Login(AuthenticationRequest userForAuthentication);
        Task Logout();
    }
}