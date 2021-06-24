using System.Threading.Tasks;
using PointOS.Common.DTO.Request;

namespace PointOS.BusinessLogic.Security
{
    public interface ITokenHandler
    {
        /// <summary>
        /// Generate User Company (Client) Authorization Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string GenerateAuthorizationToken(AuthenticationRequest request);

        /// <summary>
        /// Generate authentication token with user authentication details and roles
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<object> GenerateToken(AuthenticationRequest request);
    }
}