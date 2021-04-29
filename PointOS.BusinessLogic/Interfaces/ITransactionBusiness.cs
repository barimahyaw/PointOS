using System.Collections.Generic;
using System.Threading.Tasks;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;

namespace PointOS.BusinessLogic.Interfaces
{
    public interface ITransactionBusiness
    {
        /// <summary>
        /// Gets transaction by transaction Id and transaction type
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="transactionType"></param>
        /// <returns></returns>
        Task<SingleResponse<SaleTransactionResponse>> Find(string transactionId, TransactionType transactionType);

        /// <summary>
        /// Saves transaction base on the transaction type
        /// </summary>
        /// <param name="requests">list of transactions</param>
        /// <param name="transactionType"></param>
        /// <param name="paymentType"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResponseHeader> SaveAsync(IList<TransactionRequest> requests, TransactionType transactionType, PaymentType paymentType, string userId);
    }
}