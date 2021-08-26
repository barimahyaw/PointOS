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
    public class BranchAdapter : DataAdaptor
    {
        private readonly IApiEndpointCallService _apiEndpointCallService;
        private readonly ISessionStorageService _sessionStorageService;

        public BranchAdapter(IApiEndpointCallService apiEndpointCallService, ISessionStorageService sessionStorageService)
        {
            _apiEndpointCallService = apiEndpointCallService;
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
        {
            var session = await _sessionStorageService.GetItemAsync<UserSession>("UserSession");

            string orderByString = null;

            if (dataManagerRequest.Sorted != null)
            {
                var sortList = dataManagerRequest.Sorted;
                orderByString = string.Join(",", sortList.Select(s => $"{s.Name} {s.Direction}"));
            }

            var param = $"?companyId={session.CompanyId}&skip={dataManagerRequest.Skip}&take={dataManagerRequest.Take}&orderBy={orderByString}";

            var response = await _apiEndpointCallService.CallApiGetService("Branch", null, param);

            var result = JsonConvert.DeserializeObject<ListResponse<BranchResponse>>(response.ToString());

            var dataResult = new DataResult
            {
                Result = result.ResponseBodyList.OrderBy(x=>x.CreatedBy),
                Count = Convert.ToInt32(result.ResponseHeader.ReferenceNumber)
            };

            return dataResult;
        }
    }
}
