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
    public class SalesController : ControllerBase
    {
        private readonly ITransactionBusiness _transactionBusiness;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionBusiness"></param>
        public SalesController(ITransactionBusiness transactionBusiness)
        {
            _transactionBusiness = transactionBusiness;
        }

        /// <summary>
        /// Saves a sales transaction record(s)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseHeader> Post(IList<TransactionRequest> request)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _transactionBusiness.SaveAsync(request, TransactionType.Sales, PaymentType.Cash, user);
        }

        /// <summary>
        /// Gets a sale transaction details by transaction Id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SingleResponse<SaleTransactionResponse>> Get(string transactionId)
        {
            return await _transactionBusiness.Find(transactionId, TransactionType.Sales);
        }
    }
}
