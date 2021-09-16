using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using System.Collections.Generic;
using System.Security.Claims;
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
    public class SalesController : ControllerBase
    {
        private readonly ITransactionBusiness _transactionBusiness;
        private readonly ISalesBusiness _salesBusiness;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionBusiness"></param>
        /// <param name="salesBusiness"></param>
        public SalesController(ITransactionBusiness transactionBusiness, ISalesBusiness salesBusiness)
        {
            _transactionBusiness = transactionBusiness;
            _salesBusiness = salesBusiness;
        }

        /// <summary>
        /// Saves a sales transaction record(s)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerPhoneNumber"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseHeader> Post(IList<TransactionRequest> request, string customerPhoneNumber)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _transactionBusiness.SaveAsync(request, TransactionType.Sales, PaymentType.Cash, user, customerPhoneNumber);
        }

        /// <summary>
        /// Gets a sale transaction details by transaction Id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SingleResponse<SaleTransactionResponse>> Get(string transactionId)
            => await _transactionBusiness.Find(transactionId, TransactionType.Sales);

        /// <summary>
        /// Gets a sales details by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("getByCompany")]
        public async Task<ListResponse<SalesResponse>> GetByCompany(int companyId, int skip = 0, int take = 5)
            => await _salesBusiness.FindByCompany(companyId, skip, take);
    }
}
