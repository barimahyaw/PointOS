namespace PointOS.Common.DTO.Response
{
    public class SalesResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public string ProductCategory { get; set; }
        public double CostPrice { get; set; }
        public double WholeSalePrice { get; set; }
        public double RetailPrice { get; set; }
        public int Quantity { get; set; }

        public double RetailAmount => RetailPrice * Quantity;
        public double WholeSaleAmount => WholeSalePrice * Quantity;
        public double CostAmount => CostPrice * Quantity;
    }
}
