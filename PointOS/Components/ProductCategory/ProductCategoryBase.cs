using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using System.Threading.Tasks;

namespace PointOS.Components.ProductCategory
{
    public class ProductCategoryBase : ComponentBase
    {
        [Inject]
        private IRestUtility RestUtility { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter] public int ProductCategoryId { get; set; } = 1;

        [Inject]
        private ISnackbar Snackbar { get; set; }

        protected ProductCategoryRequest ProductCategoryRequest { get; set; } = new ProductCategoryRequest();
        protected IEnumerable<ProductCategoryResponse> ProductCategoryResponse { get; set; } = new List<ProductCategoryResponse>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>nothing/void</returns>
        protected override async Task OnInitializedAsync()
        {
            var param = $"?id={ProductCategoryId}";
            var response = await RestUtility.ApiServiceAsync(BaseUrl.PointOs, "ProductCategory/get", "", null, param, Verb.Get);

            var result = JsonConvert.DeserializeObject<ListResponse<ProductCategoryResponse>>(response.ToString());

            var responseHeader = result.ResponseHeader;
            var responseBody = result.ResponseBodyList;

            await Task.Delay(5000);

            if (responseHeader.Success)
            {
                ProductCategoryResponse = responseBody;
            }
            else
            {
                var products = new List<ProductCategoryResponse>();
                products.Add(new ProductCategoryResponse
                {
                    ProductName = "test is here",
                    CreatedOn = DateTime.UtcNow
                });

                ProductCategoryResponse = products;
                Snackbar.Add(responseHeader.Message, Severity.Error, config => config.ShowCloseIcon = true);
            }
        }


    }
}
