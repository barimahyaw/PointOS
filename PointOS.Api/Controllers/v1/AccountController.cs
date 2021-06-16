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
    public class AccountController : ControllerBase
    {
        private readonly IUserAccountBusiness _userAccountBusiness;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccountBusiness"></param>
        public AccountController(IUserAccountBusiness userAccountBusiness) => _userAccountBusiness = userAccountBusiness;

        /// <summary>
        /// User Account Authentication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public async Task<ResponseHeader> Authenticate(AuthenticationRequest request)
            => await _userAccountBusiness.AuthenticationAsync(request);

        /// <summary>
        /// User Account Authentication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ResponseHeader> Register(UserRegistrationRequest request)
            => await _userAccountBusiness.AddUser(request);
    }
}
