using PointOS.BusinessLogic.Interfaces;
using PointOS.Common.DTO.Response;
using PointOS.DataAccess;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.BusinessLogic
{
    public class DashboardBusiness : IDashboardBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardBusiness(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<SingleResponse<GeneralDashboardResponse>> General(int companyId)
        {
            var productRepo = _unitOfWork.ProductRepository;

            var sales = await _unitOfWork.SalesRepository.FindByCompanyId(companyId);
            var products = await productRepo.FindAllAsync(companyId);


            if (sales == null || products == null)
                return new SingleResponse<GeneralDashboardResponse>(new ResponseHeader(), new GeneralDashboardResponse());

            var gettingOutOfStockProducts = 0;
            var outOfStockProducts = 0;

            foreach (var salesResponse in sales)
            {
                var product = products.FirstOrDefault(p => p.Id == salesResponse.ProductId);
                var sale = sales.Where(s => s.ProductId == product?.Id);
                var productStock = products.Select(p => p.ProductCategory).Count() - sale.Count();
                if (productStock <= 10)
                    gettingOutOfStockProducts += productStock;
                if (productStock == 0)
                    outOfStockProducts += productStock;
            }

            var result = new GeneralDashboardResponse
            {
                QuantitySalesProducts = sales.Sum(s => s.Quantity),
                TotalSalesRetailAmount = sales.Sum(s => s.RetailAmount),
                TotalCostAmount = sales.Sum(s => s.CostAmount),
                GettingOutOfStockProduct = gettingOutOfStockProducts,
                OutOfStockProduct = outOfStockProducts,
                TotalNumberOfProducts = productRepo.TotalProducts(companyId)
            };

            return new SingleResponse<GeneralDashboardResponse>(new ResponseHeader { Success = true }, result);
        }


    }
}
