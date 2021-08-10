using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
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
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyBusiness _currencyBusiness;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyBusiness"></param>
        public CurrencyController(ICurrencyBusiness currencyBusiness)
            => _currencyBusiness = currencyBusiness;


        /// <summary>
        /// Gets details of all Currencies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ListResponse<CurrencyResponse>> Get()
            => await _currencyBusiness.FindAllAsync();
    }
}
