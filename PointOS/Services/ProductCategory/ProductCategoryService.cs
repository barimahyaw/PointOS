using Newtonsoft.Json;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace PointOS.Services.ProductCategory
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IRestUtility _restUtility;
        private readonly ILocalStorageService _localStorageService;

        public ProductCategoryService(IRestUtility restUtility, ILocalStorageService localStorageService)
        {
            _restUtility = restUtility;
            _localStorageService = localStorageService;
        }

        public async Task<ResponseHeader> Add(ProductCategoryRequest request)
        {
            var token = $"Bearer {await _localStorageService.GetItemAsync<string>("authToken")}";
            var response = await _restUtility.ApiServiceAsync(BaseUrl.PointOs, "ProductCategory",
                token, request, null, Verb.Post);

            var result = JsonConvert.DeserializeObject<ResponseHeader>(response.ToString());

            return result;
        }
    }
}
