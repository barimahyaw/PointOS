using Blazored.SessionStorage;
using Newtonsoft.Json;
using PointOS.Common.DTO.Response;
using PointOS.Common.DTO.Sessions;
using PointOS.Services;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using System;
using System.Threading.Tasks;

namespace PointOS.DataAdapters
{
    public class PricingAdapter : DataAdaptor
    {
        private readonly IApiEndpointCallService _apiEndpointCallService;
        private readonly ISessionStorageService _sessionStorageService;

        public PricingAdapter(IApiEndpointCallService apiEndpointCallService, ISessionStorageService sessionStorageService)
        {
            _apiEndpointCallService = apiEndpointCallService;
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
        {
            var session = await _sessionStorageService.GetItemAsync<UserSession>("UserSession");

            var param = $"?companyId={session.CompanyId}&skip={dataManagerRequest.Skip}&take={dataManagerRequest.Take}";

            var response = await _apiEndpointCallService.CallApiGetService("Pricing", null, param);

            var result = JsonConvert.DeserializeObject<ListResponse<ProductPricingResponse>>(response.ToString());

            var dataResult = new DataResult
            {
                Result = result.ResponseBodyList,
                Count = Convert.ToInt32(result.ResponseHeader.ReferenceNumber)
            };

            return dataResult;
        }
    }
}
