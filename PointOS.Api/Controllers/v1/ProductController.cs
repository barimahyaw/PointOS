using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using System;
using System.Threading.Tasks;

namespace PointOS.Api.Controllers.v1
{
    /// <summary>
    /// Product Controller Class
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;
        /// <summary>
        /// Controller DI
        /// </summary>
        /// <param name="productBusiness"></param>
        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        /// <summary>
        /// Gets all Product records 
        /// </summary>
        /// <returns>a list of products</returns>
        [HttpGet("get")]
        public async Task<ListResponse<ProductResponse>> Get() => await _productBusiness.FindAllAsync();

        /// <summary>
        /// Gets a product record by it's integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
        [HttpGet("getById")]
        public async Task<SingleResponse<ProductResponse>> GetById(int id) => await _productBusiness.FindById(id);

        /// <summary>
        /// Gets a product record by it's Guid Id/Value
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a record of product</returns>
        [HttpGet("getByValue")]
        public async Task<SingleResponse<ProductResponse>> GetById(Guid id) => await _productBusiness.FindById(id);

        /// <summary>
        /// Saves a product record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseHeader> Post(ProductRequest request) => await _productBusiness.SaveAsync(request);
    }
}
