using System;
using Blazored.SessionStorage;
using PointOS.Services;
using Syncfusion.Blazor;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PointOS.Common.DTO.Response;
using PointOS.Common.DTO.Sessions;
using Syncfusion.Blazor.Data;

namespace PointOS.Pages.Utilities.DataAdapters
{
    public class ProductCategoryAdapter : DataAdaptor
    {
        private readonly IApiEndpointCallService _apiEndpointCallService;
        private readonly ISessionStorageService _sessionStorageService;

        public ProductCategoryAdapter(IApiEndpointCallService apiEndpointCallService, ISessionStorageService sessionStorageService)
        {
            _apiEndpointCallService = apiEndpointCallService;
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
        {
            var session = await _sessionStorageService.GetItemAsync<UserSession>("UserSession");

            var param = $"?companyId={session.CompanyId}&skip={dataManagerRequest.Skip}&take={dataManagerRequest.Take}";

            var response = await _apiEndpointCallService.CallApiGetService("ProductCategory/get", null, param);

            var result = JsonConvert.DeserializeObject<ListResponse<ProductCategoryResponse>>(response.ToString());

            var dataResult = new DataResult
            {
                Result = result.ResponseBodyList,
                Count = Convert.ToInt32(result.ResponseHeader.ReferenceNumber)
            };

            return dataResult;
        }
    }
}
