namespace PointOS.Common.DTO.Response
{
    public class GeneralDashboardResponse
    {
        public int QuantitySalesProducts { get; set; }
        public double TotalSalesRetailAmount { get; set; }
        public double TotalCostAmount { get; set; }
        public double TotalSalesProfit => TotalSalesRetailAmount - TotalCostAmount;
        public int GettingOutOfStockProduct { get; set; }
        public int OutOfStockProduct { get; set; }
        public int TotalNumberOfProducts { get; set; }
    }
}