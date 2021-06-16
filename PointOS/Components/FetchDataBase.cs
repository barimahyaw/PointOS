using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using System.Threading.Tasks;

namespace PointOS.Components
{
    public class FetchDataBase : ComponentBase
    {
        //[Inject]
        //public IRegistrationService RegistrationService { get; set; }

        [Inject]
        private IRestUtility RestUtility { get; set; }

        public ListResponse<ProductCategoryResponse> ProductCategoryResponses { get; set; }
        protected string EventHandlerTest { get; set; } = "My first Test";
        protected string Coordinates { get; set; }
        protected string Description { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var response = await RestUtility.ApiServiceAsync(BaseUrl.PointOs, "ProductCategory/get",
                null, null, null, Verb.Get);

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ListResponse<ProductCategoryResponse>>(response.ToString());

            //ProductCategoryResponses = result.ResponseHeader.Success
            //? result.ResponseBodyList : new List<ProductCategoryResponse>();
            //ProductCategoryResponses = await RegistrationService.ProductCategories();

            ProductCategoryResponses = result;
        }

        protected void ButtonHover(MouseEventArgs e)
        {
            EventHandlerTest = "This is event Handler test";
            Coordinates = $"X- {e.ClientX} & Y {e.ClientY}";
        }

    }
}
