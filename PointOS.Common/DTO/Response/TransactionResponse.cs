namespace PointOS.Common.DTO.Response
{
    public class TransactionResponse : ResponseBody
    {
        public string TransactionId { get; set; }
        public string TransactionType { get; set; }
        public string PaymentType { get; set; }
        public double Amount { get; set; }
    }
}
