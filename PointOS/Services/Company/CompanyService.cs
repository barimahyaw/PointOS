using Blazored.LocalStorage;
using Newtonsoft.Json;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using System.Threading.Tasks;

namespace PointOS.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly IRestUtility _restUtility;
        private readonly ILocalStorageService _localStorageService;

        public CompanyService(IRestUtility restUtility, ILocalStorageService localStorageService)
        {
            _restUtility = restUtility;
            _localStorageService = localStorageService;
        }

        public async Task<ResponseHeader> Add(CompanyRegistrationRequest request)
        {
            var token = $"Bearer {await _localStorageService.GetItemAsync<string>("authToken")}";
            var response = await _restUtility.ApiServiceAsync(BaseUrl.PointOs, "Company",
                token, request, null, Verb.Post);

            var result = JsonConvert.DeserializeObject<ResponseHeader>(response.ToString());

            return result;
        }
    }
}
