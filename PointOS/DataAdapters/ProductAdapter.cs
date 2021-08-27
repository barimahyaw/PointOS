using Blazored.SessionStorage;
using Newtonsoft.Json;
using PointOS.Common.DTO.Response;
using PointOS.Common.DTO.Sessions;
using PointOS.Services;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAdapters
{
    public class ProductAdapter : DataAdaptor
    {
        private readonly IApiEndpointCallService _apiEndpointCallService;
        private readonly ISessionStorageService _sessionStorageService;

        public ProductAdapter(IApiEndpointCallService apiEndpointCallService, ISessionStorageService sessionStorageService)
        {
            _apiEndpointCallService = apiEndpointCallService;
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
        {
            var session = await _sessionStorageService.GetItemAsync<UserSession>("UserSession");

            string searchString = null;

            if (dataManagerRequest.Search != null)
            {
                var searchList = dataManagerRequest.Search;
                searchString = string.Join(",", searchList.Select(s => $"{s.Key}"));
            }

            var param = $"?companyId={session.CompanyId}&skip={dataManagerRequest.Skip}&take={dataManagerRequest.Take}&search={searchString}";

            var response = await _apiEndpointCallService.CallApiGetService("Product", null, param);

            var result = JsonConvert.DeserializeObject<ListResponse<ProductResponse>>(response.ToString());

            var dataResult = new DataResult
            {
                Result = result.ResponseBodyList,
                Count = Convert.ToInt32(result.ResponseHeader.ReferenceNumber)
            };

            return dataResult;
        }
    }
}
