using Blazored.LocalStorage;
using Newtonsoft.Json;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using System.Threading.Tasks;

namespace PointOS.Services
{
    public class ApiEndpointCallService : IApiEndpointCallService
    {
        private readonly IRestUtility _restUtility;
        private readonly ILocalStorageService _localStorageService;

        public ApiEndpointCallService(IRestUtility restUtility, ILocalStorageService localStorageService)
        {
            _restUtility = restUtility;
            _localStorageService = localStorageService;
        }

        /// <summary>
        /// Generic method to call api endpoints
        /// </summary>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <param name="param"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<ResponseHeader> CallApiService(string url, object request, string param, Verb method)
        {
            var token = $"Bearer {await _localStorageService.GetItemAsync<string>("authToken")}";

            var response = await _restUtility.ApiServiceAsync(BaseUrl.PointOs, url, token, request, param, method);

            if (response == null)
            {
                return new ResponseHeader
                {
                    Message = "Opss!!! Something went wrong. Try again later or contact System Administrator for assistance"
                };
            }

            var result = JsonConvert.DeserializeObject<ResponseHeader>(response.ToString());

            return result;
        }
    }
}
