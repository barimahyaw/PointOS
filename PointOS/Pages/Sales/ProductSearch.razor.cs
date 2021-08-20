using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using PointOS.Common.DTO.Response;
using PointOS.Services;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.Pages.Sales
{
    public partial class ProductSearch
    {
        public string AvatarImageLink { get; set; } = "images/avatar_jonny.jpg";
        public string AvatarIcon { get; set; } = Icons.Material.Outlined.SentimentVeryDissatisfied;

        public string[] PagerDropDown { get; set; } = { "All", "5", "10", "15", "20" };

        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        [Parameter]
        public EventCallback ProductsChanged { get; set; }

        [Parameter]
        public IList<ProductResponse> Products { get; set; } = new List<ProductResponse>();

        [Inject]
        private IDialogService DialogService { get; set; }

        /// <summary>
        /// invoke event call back ProductsChanged
        /// </summary>
        /// <returns></returns>
        protected async Task OnConfirmationChange() => await ProductsChanged.InvokeAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task ActionBeginHandler(RowSelectEventArgs<ProductResponse> args)
        {
            if (Products.All(p => p.Id != args.Data.Id))
            {
                if (args.Data?.Stock > 0)
                {
                    var param = $"?id={args.Data.Id}";
                    var response = await ApiEndpointCallService.CallApiGetService("Product/get", null, param);

                    var result = JsonConvert.DeserializeObject<SingleResponse<ProductResponse>>(response.ToString());

                    Products.Add(result.ResponseBody);
                    await OnConfirmationChange();
                }
                else
                {
                    Dialog("Stock Empty", "Product is out of Stock");
                }

            }
            else
            {
                Dialog("Alert", "Product is already selected");
            }

        }

        /// <summary>
        /// Generic wrapper to call the dialog component
        /// </summary>
        /// <param name="header"></param>
        /// <param name="content"></param>
        public void Dialog(string header, string content)
        {
            var parameters = new DialogParameters { { "ContentText", content } };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            DialogService.Show<Utilities.Dialog>(header, parameters, options);
        }
    }
}
