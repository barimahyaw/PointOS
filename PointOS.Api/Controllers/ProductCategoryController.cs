using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using System;
using System.Threading.Tasks;

namespace PointOS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryBusiness _productCategoryBusiness;

        public ProductCategoryController(IProductCategoryBusiness productCategoryBusiness)
        {
            _productCategoryBusiness = productCategoryBusiness;
        }

        /// <summary>
        /// Select a record of product category by it's Guid Id or integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="guidValue"></param>
        /// <returns>a single product category record</returns>
        [HttpGet("getProductCategory")]
        public async Task<SingleResponse<ProductCategoryResponse>> GetProductCategory(Guid? guidValue, int? id)
        {
            return await _productCategoryBusiness.GetProductCategory(id, guidValue);
        }

        /// <summary>
        /// Select all records of product category 
        /// </summary>
        /// <returns>a list of product category records</returns>
        [HttpGet("getProductCategories")]
        public async Task<ListResponse<ProductCategoryResponse>> GetProductCategories()
        {
            return await _productCategoryBusiness.FindAllAsync();
        }

        /// <summary>
        /// Saves a product category record
        /// </summary>
        /// <param name="request"></param>
        /// <returns>number of records affected</returns>
        public async Task<ResponseHeader> Post(ProductCategoryRequest request)
        {
            return await _productCategoryBusiness.SaveAsync(request);
        }
    }
}
