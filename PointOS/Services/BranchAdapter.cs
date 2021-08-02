using Blazored.SessionStorage;
using Newtonsoft.Json;
using PointOS.Common.DTO.Response;
using PointOS.Common.DTO.Sessions;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.Services
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

            var param = $"?companyId={session.CompanyId}&skip={dataManagerRequest.Skip}&take={dataManagerRequest.Take}";

            var response = await _apiEndpointCallService.CallApiGetService("Branch", null, param);

            var result = JsonConvert.DeserializeObject<ListResponse<BranchResponse>>(response.ToString());

            //var branchResults = result.ResponseBodyList
            //    .Select(branchResponse => new BranchResponse
            //    {
            //        Name = branchResponse.Name,
            //        Id = branchResponse.Id,
            //        CreatedBy = branchResponse.CreatedBy,
            //        CreatedOn = branchResponse.CreatedOn
            //    }).ToList();

            var dataResult = new DataResult
            {
                Result = result.ResponseBodyList,
                Count = result.ResponseBodyList.Count()
            };
            await Task.Delay(3000);

            return dataResult;
        }
    }
}
