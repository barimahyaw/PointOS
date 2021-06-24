using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;

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
        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }

        protected ProductCategoryRequest ProductCategoryRequest { get; set; } = new ProductCategoryRequest();
        protected IEnumerable<ProductCategoryResponse> ProductCategoryResponse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>nothing/void</returns>
        protected override async Task OnInitializedAsync()
        {
            var param = $"?id={ProductCategoryId}";

            var token = await LocalStorage.GetItemAsync<string>("authToken");

            var response = await RestUtility.ApiServiceAsync(BaseUrl.PointOs, "ProductCategory/get", token, null, param, Verb.Get);

            var result = JsonConvert.DeserializeObject<ListResponse<ProductCategoryResponse>>(response.ToString());

            var responseHeader = result.ResponseHeader;
            var responseBody = result.ResponseBodyList;

            if (responseHeader.Success)
            {
                ProductCategoryResponse = responseBody;
            }
            else
            {
                await Task.Delay(5000);
                var products = new List<ProductCategoryResponse>();
                products.Add(new ProductCategoryResponse
                {
                    ProductName = "test is here",
                    CreatedOn = DateTime.UtcNow
                });
                products.Add(new ProductCategoryResponse
                {
                    ProductName = "test is here",
                    CreatedOn = DateTime.UtcNow
                });
                products.Add(new ProductCategoryResponse
                {
                    ProductName = "test is here",
                    CreatedOn = DateTime.UtcNow
                });
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
