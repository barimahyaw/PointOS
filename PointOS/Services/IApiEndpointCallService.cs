using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using System.Threading.Tasks;

namespace PointOS.Services
{
    public interface IApiEndpointCallService
    {
        /// <summary>
        /// Generic method to call api endpoints
        /// </summary>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <param name="param"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        Task<ResponseHeader> CallApiService(string url, object request, string param, Verb method);
    }
}