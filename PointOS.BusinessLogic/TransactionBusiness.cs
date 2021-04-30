using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Request;
using PointOS.Common.DTO.Response;
using PointOS.Common.Enums;
using PointOS.Common.Extensions;
using PointOS.Common.Helpers.IHelpers;
using PointOS.DataAccess;
using PointOS.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class TransactionBusiness : ITransactionBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtils _utils;

        public TransactionBusiness(IUnitOfWork unitOfWork, IUtils utils)
        {
            _unitOfWork = unitOfWork;
            _utils = utils;
        }

        /// <summary>
        /// Gets transaction by transaction Id and transaction type
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="transactionType"></param>
        /// <returns></returns>
        public async Task<SingleResponse<SaleTransactionResponse>> Find(string transactionId, TransactionType transactionType)
        {
            switch (transactionType)
            {
                case TransactionType.Sales:
                    return await FindSaleTransactions(transactionId);
                case TransactionType.Payment:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null);
            }
            return new SingleResponse<SaleTransactionResponse>(new ResponseHeader { Message = "Transaction Type is not specified" }, null);
        }

        /// <summary>
        /// Gets Sales transaction by transaction Id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        private async Task<SingleResponse<SaleTransactionResponse>> FindSaleTransactions(string transactionId)
        {
            var sales = await _unitOfWork.SalesRepository.FindByTransactionId(transactionId);
            var tran = await _unitOfWork.TransactionRepository.FindByTransactionId(transactionId);

            var tranResponse = new SaleTransactionResponse { SalesResponses = sales, TransactionResponse = tran };

            return tranResponse.TransactionResponse != null
                ? new SingleResponse<SaleTransactionResponse>(new ResponseHeader { Success = true }, tranResponse)
                : new SingleResponse<SaleTransactionResponse>(new ResponseHeader { Message = "No record found" }, null);
        }

        /// <summary>
        /// Saves transaction base on the transaction type
        /// </summary>
        /// <param name="requests">list of transactions</param>
        /// <param name="transactionType"></param>
        /// <param name="paymentType"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResponseHeader> SaveAsync(IList<TransactionRequest> requests, TransactionType transactionType, PaymentType paymentType, string userId)
        {
            var transactionId = _utils.GenerateTransactionTicket();

            switch (transactionType)
            {
                case TransactionType.Sales:
                    await SalesTransaction(requests, transactionId);
                    break;
                case TransactionType.Payment:
                    break;
                default:
                    goto case TransactionType.Sales;
            }

            var trans = new Transactions
            {
                TransactionType = TransactionType.Sales.GetAttributeStringValue(),
                TransactionId = transactionId,
                Amount = requests.Sum(x => x.Amount),
                PaymentType = paymentType.GetAttributeStringValue(),
                CreatedUserId = userId,
                CreatedOn = DateTime.UtcNow
            };

            await _unitOfWork.TransactionRepository.AddAsync(trans);

            var numRow = await _unitOfWork.SaveChangesAsync();

            return numRow > 0 ? new ResponseHeader { Success = true } : new ResponseHeader();
        }

        /// <summary>
        /// private method to save sales transaction
        /// </summary>
        /// <param name="requests"></param>
        /// <param name="transactionId"></param>
        /// <returns>nothing/void</returns>
        private async Task SalesTransaction(IEnumerable<TransactionRequest> requests, string transactionId)
        {
            var trans = requests.Select(t => new Sales
            {
                TransactionId = transactionId,
                Quantity = t.Quantity,
                ProductPricingId = t.ProductPricingId
            }).ToList();

            await _unitOfWork.SalesRepository.AddAsync(trans);
        }
    }
}
