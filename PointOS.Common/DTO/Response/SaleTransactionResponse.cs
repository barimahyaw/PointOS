using System.Collections.Generic;

namespace PointOS.Common.DTO.Response
{
    public class SaleTransactionResponse
    {
        public TransactionResponse TransactionResponse { get; set; }
        public IList<SalesResponse> SalesResponses { get; set; }
    }
}
