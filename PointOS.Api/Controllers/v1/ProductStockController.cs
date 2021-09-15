using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using System.Threading.Tasks;

namespace PointOS.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "CustomAuthentication")]
    public class ProductStockController : ControllerBase
    {
        private readonly IProductStockBusiness _productStockBusiness;

        /// <summary>
        /// Controller constructor for DI
        /// </summary>
        /// <param name="productStockBusiness"></param>
        public ProductStockController(IProductStockBusiness productStockBusiness) => _productStockBusiness = productStockBusiness;

        /// <summary>
        /// Saves a new product's stock record
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseHeader> Post(ProductStockRequest request)
            => await _productStockBusiness.SaveAsync(request);
    }
}
