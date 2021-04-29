namespace PointOS.Common.DTO.Response
{
    public class SalesResponse
    {
        public string Product { get; set; }
        public string ProductCategory { get; set; }
        public double CostPrice { get; set; }
        public double WholeSalePrice { get; set; }
        public double RetailPrice { get; set; }
        public int Quantity { get; set; }

        public double RetailAmount => WholeSalePrice * Quantity;
        public double WholeSaleAmount => RetailPrice * Quantity;
    }
}
