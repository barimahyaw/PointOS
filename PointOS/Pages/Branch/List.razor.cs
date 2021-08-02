//namespace PointOS.Pages.Branch
//{
//    public partial class List
//    {
//        public string[] PagerDropDown { get; set; } = { "All", "5", "10", "15", "20" };
//        //[Inject]
//        //protected ISessionStorageService SessionStorageService { get; set; }
//        //[Inject]
//        //private IApiEndpointCallService ApiEndpointCallService { get; set; }

//        //private IEnumerable<BranchResponse> BranchResponse { get; set; } = new List<BranchResponse>();
//        //protected override async Task OnInitializedAsync()
//        //{
//        //    var session = await SessionStorageService.GetItemAsync<UserSession>("UserSession");
//        //    var param = $"?companyId={session.CompanyId}";

//        //    var response = await ApiEndpointCallService.CallApiGetService("Branch", null, param);

//        //    var result = new ListResponse<BranchResponse>();

//        //    if (response != null)
//        //    {
//        //        result = JsonConvert.DeserializeObject<ListResponse<BranchResponse>>(response.ToString());
//        //    }

//        //    BranchResponse = result.ResponseBodyList;
//        //}
//    }
//}
