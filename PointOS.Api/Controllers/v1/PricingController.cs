using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using System.Threading.Tasks;

namespace PointOS.Api.Controllers.v1
{
    /// <summary>
    /// Product pricing controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "CustomAuthentication")]
    public class PricingController : ControllerBase
    {
        private readonly IProductPricingBusiness _productPricingBusiness;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productPricingBusiness"></param>
        public PricingController(IProductPricingBusiness productPricingBusiness)
        {
            _productPricingBusiness = productPricingBusiness;
        }

        /// <summary>
        /// Saves a product pricing record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseHeader> Post(ProductPricingRequest request)
        {
            request.Operation = CrudOperation.Create;
            return await _productPricingBusiness.SaveAsync(request);
        }

        /// <summary>
        /// Updates a product pricing record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResponseHeader> Put(ProductPricingRequest request)
        {
            request.Operation = CrudOperation.Update;
            return await _productPricingBusiness.SaveAsync(request);
        }
    }
}
