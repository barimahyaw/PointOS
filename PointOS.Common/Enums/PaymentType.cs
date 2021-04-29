using PointOS.Common.Attributes;

namespace PointOS.Common.Enums
{
    public enum PaymentType
    {
        [StringValue("Cash Payment")]
        Cash = 1,
        [StringValue("Mobile Money Payment")]
        MobileMoney,
        [StringValue("Cheque Payment")]
        Cheque,
        [StringValue("Card Payment")]
        Card
    }
}
